import { useState, useEffect } from 'react'
import { useSettingsStore } from '../stores/settingsStore'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'
import InputGroup from 'react-bootstrap/InputGroup'
import ListGroup from 'react-bootstrap/ListGroup'

function OmitTickerList(props) {
    // State variables from global store
    const {
        toOmit,
        setToOmit
    } = useSettingsStore()

    // Page specific form state variables
    const [newTickerToOmit, setNewTickerToOmit] = useState('')
    const [settingsLoaded, setSettingsLoaded] = useState(false)

    useEffect(() => {
        if (settingsLoaded !== props.settingsLoaded) {
            setSettingsLoaded(props.settingsLoaded)
        }
    }, [props.settingsLoaded])

    return (
        <>
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
        </>
    )
}

export default OmitTickerList