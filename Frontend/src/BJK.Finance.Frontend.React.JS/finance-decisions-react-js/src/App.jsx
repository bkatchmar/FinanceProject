import { useState, useEffect } from 'react'
import axios from 'axios'
import Accordion from 'react-bootstrap/Accordion'
import Container from 'react-bootstrap/Container'
import Form from 'react-bootstrap/Form'
import InputGroup from 'react-bootstrap/InputGroup'

function App() {
  const API_URL = import.meta.env.VITE_API_URL
  const [settingsLoaded, setSettingsLoaded] = useState(false)
  const [uninvested, setUninvested] = useState(0.0)
  const [minimum, setMinimum] = useState(0)
  const [ratings, setRatings] = useState(["Buy", "Strong Buy", "Hold", "N/A"])

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await axios.get(`${API_URL}/DefaultSettings`)
        setSettingsLoaded(true)
        setUninvested(result["data"]["personalData"]["uninvestedCash"])
        setMinimum(result["data"]["personalData"]["minumumUnitsToBuy"])
        setRatings(result["data"]["personalData"]["ratingsTolerance"])
      } catch (error) {
        console.error('Error fetching data:', error)
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
                    e.preventDefault()
                    setUninvested(e.target.value)
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
                    e.preventDefault()
                    let val = e.target.value

                    // Remove any non-digit characters (including '.')
                    val = val.replace(/[^\d]/g, '')
                    setMinimum(val)
                  }} />
              </InputGroup>
              <Form.Group>
                <Form.Label>Ratings Tolerance</Form.Label>
                <Form.Check
                  type="checkbox"
                  label={"Strong Buy"}
                  checked={ratings.includes("Strong Buy")}
                  onChange={(e) => {
                    const rating = "Strong Buy"
                    if (e.target.checked) {
                      setRatings([...ratings, rating])
                    } else {
                      setRatings(ratings.filter((v) => v !== rating))
                    }
                  }}
                />
              </Form.Group>
            </Form>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
    </Container>
  )
}

export default App
