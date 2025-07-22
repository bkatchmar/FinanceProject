import { useState, useEffect } from 'react'
import Spinner from 'react-bootstrap/Spinner'
import Table from 'react-bootstrap/Table'

function StrategyMoveTable(props) {
    const [newStrategyMoves, setNewStrategyMoves] = useState([])
    const [loadingData, setLoadingData] = useState(false)

    useEffect(() => {
        if (props.loadingData !== loadingData) {
            setLoadingData(props.loadingData)
        }
    }, [props.loadingData])

    useEffect(() => {
        if (props.newStrategyMoves.length !== newStrategyMoves.length) {
            setNewStrategyMoves(props.newStrategyMoves)
        }
    }, [props.newStrategyMoves])

    if (loadingData) {
        return <Spinner className="d-block mt-5" animation="border" variant="info" />
    } else if (newStrategyMoves.length > 0 && !loadingData) {
        return (<Table responsive striped bordered hover size="sm" className="mt-2">
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
        </Table>)
    }

    return null
}

export default StrategyMoveTable