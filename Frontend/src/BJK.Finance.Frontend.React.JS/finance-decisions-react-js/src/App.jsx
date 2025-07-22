import { useState, useEffect } from 'react'
import { useSettingsLoad } from './hooks/useSettingsLoad'
import { useSettingsStore } from './stores/settingsStore'
import SettingsForm from './components/SettingsForm'
import OmitTickerList from './components/OmitTickerList'
import StrategyMoveTable from './components/StrategyMoveTable'
import axios from 'axios'
import Accordion from 'react-bootstrap/Accordion'
import Button from 'react-bootstrap/Button'
import Container from 'react-bootstrap/Container'

function App() {
  const API_URL = import.meta.env.VITE_API_URL
  const { settingsLoaded, initialLoadSettings } = useSettingsLoad(`${API_URL}/DefaultSettings`)

  // State variables from global store
  const {
    uninvested,
    minimum,
    ratings,
    strategies,
    toOmit,
    setUninvested,
    setMinimum,
    setRatings,
    setToOmit
  } = useSettingsStore()

  // Page specific form state variables
  const [newStrategyMoves, setNewStrategyMoves] = useState([])
  const [loadingData, setLoadingData] = useState(false)

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => { }, [])

  // `settingsLoaded` will be true once the custom hook has completed
  useEffect(() => {
    if (settingsLoaded) {
      setUninvested(initialLoadSettings.uninvested)
      setMinimum(initialLoadSettings.minimum)
      setRatings(initialLoadSettings.ratings)
      setToOmit(initialLoadSettings.toOmit)
    }
  }, [settingsLoaded, initialLoadSettings])

  return (
    <Container fluid>
      <Accordion className="mt-3">
        <Accordion.Item eventKey="0">
          <Accordion.Header>Settings</Accordion.Header>
          <Accordion.Body>
            <SettingsForm settingsLoaded={settingsLoaded} />
          </Accordion.Body>
        </Accordion.Item>
        <Accordion.Item eventKey="1">
          <Accordion.Header>Tickers To Omit</Accordion.Header>
          <Accordion.Body>
            <OmitTickerList settingsLoaded={settingsLoaded} />
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
      <Button variant="info" className='mt-1' disabled={loadingData} onClick={async () => {
        const requestBody = {
          tickersToOmit: toOmit,
          personalData: {
            uninvestedCash: uninvested,
            minumumUnitsToBuy: minimum,
            ratingsTolerance: ratings
          },
          includeCoverCalls: strategies.includes("Cover Call"),
          includeCashSecuredPuts: strategies.includes("Cash Secured Put")
        }

        setLoadingData(true)
        try {
          const response = await axios.post(`${API_URL}/Data`, requestBody)
          setNewStrategyMoves(response.data)
        } catch (error) {
          console.error('Error posting data:', error)
        } finally {
          setLoadingData(false)
        }
      }}>
        Get Data
      </Button>
      <StrategyMoveTable loadingData={loadingData} newStrategyMoves={newStrategyMoves} />
    </Container>
  )
}

export default App