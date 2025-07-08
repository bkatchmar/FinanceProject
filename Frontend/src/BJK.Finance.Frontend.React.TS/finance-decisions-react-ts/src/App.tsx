import { useState, useEffect } from 'react'
import axios from 'axios'
import Accordion from 'react-bootstrap/Accordion'
import Button from 'react-bootstrap/Button'
import Container from 'react-bootstrap/Container'
import Form from 'react-bootstrap/Form'
import InputGroup from 'react-bootstrap/InputGroup'
import ListGroup from 'react-bootstrap/ListGroup'
import Spinner from 'react-bootstrap/Spinner'
import Table from 'react-bootstrap/Table'
import { type DefaultSettings } from "./models/settings"
import { type Strategy } from "./models/strategy"
import { type GetStrategiesRequest } from "./models/strategies.get.request"

function App() {
  const API_URL: string = import.meta.env.VITE_API_URL
  const STRATEGIES: string[] = ["Cover Call", "Cash Secured Put"]
  const RATINGS: string[] = ["Strong Buy", "Buy", "Hold", "N/A", "Underperform"]

  const [settingsLoaded, setSettingsLoaded] = useState<boolean>(false)
  const [uninvested, setUninvested] = useState<number>(0.0)
  const [minimum, setMinimum] = useState<number>(0)
  const [ratings, setRatings] = useState<string[]>(["Buy", "Strong Buy", "Hold", "N/A", "Underperform"])
  const [strategies, setStrategies] = useState<string[]>(["Cash Secured Put"])
  const [toOmit, setToOmit] = useState<string[]>([])
  const [newTickerToOmit, setNewTickerToOmit] = useState<string>('')
  const [newStrategyMoves, setNewStrategyMoves] = useState<Strategy[]>([])
  const [loadingData, setLoadingData] = useState<boolean>(false)

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await axios.get<DefaultSettings>(`${API_URL}/DefaultSettings`)
        setUninvested(result.data.personalData.uninvestedCash)
        setMinimum(result.data.personalData.minumumUnitsToBuy)
        setRatings(result.data.personalData.ratingsTolerance)
        setToOmit(result.data.tickersToOmit)
        // tickersToOmit
      } catch (error) {
        console.error('Error fetching data:', error)
        setUninvested(1000)
        setMinimum(100)
        setRatings(["Buy", "Strong Buy", "Hold", "N/A", "Underperform"])
        setToOmit([])
      } finally {
        setSettingsLoaded(true)
      }
    }

    if (!settingsLoaded) {
      fetchData()
    }
  }, [])

  return (
    <Container fluid>
      <Accordion className="mt-3">
        <Accordion.Item eventKey="0">
          <Accordion.Header>Settings</Accordion.Header>
          <Accordion.Body>
            <Form>
              <Form.Label htmlFor="uninvested">Uninvested Cash</Form.Label>
              <InputGroup>
                <InputGroup.Text>$</InputGroup.Text>
                <Form.Control
                  id="uninvested"
                  disabled={!settingsLoaded}
                  value={uninvested}
                  type="number"
                  min="0"
                  aria-label="How much in uninvested cash?"
                  onChange={(e) => {
                    setUninvested(Number(e.target.value))
                  }} />
              </InputGroup>
              <Form.Label htmlFor="minimum">Minimum To Afford</Form.Label>
              <InputGroup>
                <Form.Control
                  id="minimum"
                  disabled={!settingsLoaded}
                  value={minimum}
                  type="number"
                  min="0"
                  step="1"
                  aria-label="How many do you want to afford?"
                  onChange={(e) => {
                    let val = e.target.value

                    // Remove any non-digit characters (including '.')
                    val = val.replace(/[^\d]/g, '')
                    setMinimum(Number(val))
                  }} />
              </InputGroup>
              <Form.Group className="mt-2 mb-1">
                <Form.Label>Ratings Tolerance</Form.Label>
                {RATINGS.map((rating, index) => {
                  return <Form.Check
                    type="checkbox"
                    label={rating}
                    checked={ratings.includes(rating)}
                    disabled={!settingsLoaded}
                    key={`rating-${index}`}
                    onChange={(e) => {
                      if (e.target.checked) {
                        setRatings([...ratings, rating])
                      } else {
                        setRatings(ratings.filter((v) => v !== rating))
                      }
                    }}
                  />
                })}
              </Form.Group>
              <Form.Group className="mt-2">
                <Form.Label>Strategies To Include</Form.Label>
                {STRATEGIES.map((strat, index) => {
                  return <Form.Check
                    type="checkbox"
                    label={strat}
                    checked={strategies.includes(strat)}
                    key={`strat-${index}`}
                    onChange={(e) => {
                      if (e.target.checked) {
                        setStrategies([...strategies, strat])
                      } else {
                        setStrategies(strategies.filter((v) => v !== strat))
                      }
                    }}
                  />
                })}
              </Form.Group>
            </Form>
          </Accordion.Body>
        </Accordion.Item>
        <Accordion.Item eventKey="1">
          <Accordion.Header>Tickers To Omit</Accordion.Header>
          <Accordion.Body>
            <Form className="mb-2" onSubmit={(e) => {
              e.preventDefault()
              if (newTickerToOmit.trim() !== '') {
                setToOmit([...toOmit, newTickerToOmit])
                setNewTickerToOmit('')
              }
            }}>
              <Form.Label htmlFor="newticker">Add Ticker To Ignore List</Form.Label>
              <InputGroup>
                <Form.Control
                  id="newticker"
                  value={newTickerToOmit}
                  aria-label="Type in new ticker"
                  onChange={(e) => {
                    setNewTickerToOmit(e.target.value)
                  }} />
              </InputGroup>
              <Button type="submit" variant="primary" className="mt-1" disabled={newTickerToOmit.trim() === ''}>
                Add
              </Button>
            </Form>
            <ListGroup>
              {toOmit.map((ticker, i) => (
                <ListGroup.Item key={i}>{ticker}
                  <Button variant="link" size="sm" className="float-end" onClick={(e) => {
                    e.preventDefault()
                    setToOmit(toOmit.filter((v) => v !== ticker))
                  }}>
                    <i className="bi bi-trash"></i>
                  </Button>
                </ListGroup.Item>
              ))}
            </ListGroup>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
      <Button variant="info" className='mt-1' disabled={loadingData} onClick={async () => {
        const requestBody: GetStrategiesRequest = {
          "tickersToOmit": toOmit,
          "personalData": {
            "uninvestedCash": uninvested,
            "minumumUnitsToBuy": minimum,
            "ratingsTolerance": ratings
          },
          "includeCoverCalls": strategies.includes("Cover Call"),
          "includeCashSecuredPuts": strategies.includes("Cash Secured Put")
        }

        setLoadingData(true)
        try {
          const response = await axios.post<Strategy[]>(`${API_URL}/Data`, requestBody)
          setNewStrategyMoves(response.data)
        } catch (error) {
          console.error('Error posting data:', error)
        } finally {
          setLoadingData(false)
        }
      }}>Get Data</Button>
      {loadingData && <Spinner className="d-block mt-5" animation="border" variant="info" />}
      {newStrategyMoves.length > 0 && !loadingData && <Table responsive striped bordered hover size="sm" className="mt-2">
        <thead>
          <tr>
            <th>Ticker</th>
            <th className="text-center">Asset Name</th>
            <th className="text-center">Rating</th>
            <th className="text-center">Strategy</th>
            <th className="text-center">Can Afford</th>
          </tr>
        </thead>
        <tbody>
          {newStrategyMoves.map((move, i) => (
            <tr key={`strat-${i}`}>
              <td>{move.symbol}</td>
              <td className="text-center">{move.name}</td>
              <td className="text-center">{move.analystRating}</td>
              <td className="text-center">{move.typeOfStrategy}</td>
              <td className="text-center">{move.contractsCanAfford}</td>
            </tr>
          ))}
        </tbody>
      </Table>}
    </Container>
  )
}

export default App
