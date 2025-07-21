import { useState, useEffect } from 'react'
import axios from 'axios'

export function useSettingsLoad(url) {
    const [settingsLoaded, setSettingsLoaded] = useState(false)
    const [initialLoadSettings, setInitialLoadSettings] = useState({
        "uninvested": 0.0,
        "minimum": 0,
        "ratings": ["Buy", "Strong Buy", "Hold", "N/A", "Underperform"],
        "toOmit": []
    })

    useEffect(() => {
        axios.get(url).then((response) => {
            setInitialLoadSettings({
                "uninvested": response.data.personalData.uninvestedCash,
                "minimum": response.data.personalData.minumumUnitsToBuy,
                "ratings": response.data.personalData.ratingsTolerance,
                "toOmit": response.data.tickersToOmit
            })
        }).catch((error) => {
            console.error(error)
        }).finally(() => {
            setSettingsLoaded(true)
        })
    }, [url])

    return { settingsLoaded, initialLoadSettings }
}