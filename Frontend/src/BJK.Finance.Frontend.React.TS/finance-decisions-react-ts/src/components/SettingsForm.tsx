import { useState, useEffect } from 'react'
import { useSettingsStore } from '../stores/useSettingsStore'
import Form from 'react-bootstrap/Form'
import InputGroup from 'react-bootstrap/InputGroup'

interface SettingsFormProps {
    settingsLoaded: boolean
}

function SettingsForm(props: SettingsFormProps) {
    const ALL_RATINGS = ["Buy", "Strong Buy", "Hold", "N/A", "Underperform"]
    const ALL_STRATEGIES = ["Cover Call", "Cash Secured Put"]

    // State variables from global store
    const {
        uninvested,
        minimum,
        ratings,
        strategies,
        setUninvested,
        setMinimum,
        setRatings,
        setStrategies
    } = useSettingsStore()

    const [settingsLoaded, setSettingsLoaded] = useState(false)

    useEffect(() => {
        if (settingsLoaded !== props.settingsLoaded) {
            setSettingsLoaded(props.settingsLoaded)
        }
    }, [props.settingsLoaded])

    return (
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
                {ALL_RATINGS.map((rating, rIndex) => <Form.Check
                    key={`rating-radio-${rIndex}`}
                    type="checkbox"
                    label={rating}
                    checked={ratings.includes(rating)}
                    disabled={!settingsLoaded}
                    onChange={(e) => {
                        if (e.target.checked) {
                            setRatings([...ratings, rating])
                        } else {
                            setRatings(ratings.filter((v) => v !== rating))
                        }
                    }}
                />)}
            </Form.Group>
            <Form.Group className="mt-2">
                <Form.Label>Strategies To Include</Form.Label>
                {ALL_STRATEGIES.map((strategy, sIndex) => <Form.Check
                    key={`strat-i-${sIndex}`}
                    type="checkbox"
                    label={strategy}
                    checked={strategies.includes(strategy)}
                    onChange={(e) => {
                        if (e.target.checked) {
                            setStrategies([...strategies, strategy])
                        } else {
                            setStrategies(strategies.filter((v) => v !== strategy))
                        }
                    }}
                />)}
            </Form.Group>
        </Form>
    )
}

export default SettingsForm