import { useState, useEffect } from 'react'
import { useSettingsLoad } from './hooks/useSettingsLoad'
import { useSettingsStore } from './stores/useSettingsStore'
import SettingsForm from './components/SettingsForm'
import OmitTickerList from './components/OmitTickerList'
import StrategyMoveTable from './components/StrategyMoveTable'
import axios from 'axios'
import Accordion from 'react-bootstrap/Accordion'
import Button from 'react-bootstrap/Button'
import Container from 'react-bootstrap/Container'
import { type Strategy } from "./models/strategy"
import { type GetStrategiesRequest } from "./models/strategies.get.request"

function App() {
  const API_URL: string = import.meta.env.VITE_API_URL
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
  const [newStrategyMoves, setNewStrategyMoves] = useState<Strategy[]>([])
  const [loadingData, setLoadingData] = useState<boolean>(false)

  // Similar to componentDidMount and componentDidUpdate:
  useEffect(() => { }, [])

  useEffect(() => {
    if (settingsLoaded) {
      setUninvested(initialLoadSettings.uninvested)
      setMinimum(initialLoadSettings.minimum)
      setRatings(initialLoadSettings.ratings)
      setToOmit(initialLoadSettings.toOmit)
    }
  }, [settingsLoaded])

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
        const requestBody: GetStrategiesRequest = {
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
          const response = await axios.post<Strategy[]>(`${API_URL}/Data`, requestBody)
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
