import { useState, useEffect } from 'react'
import axios from 'axios'
import { type DefaultSettings } from '../models/settings'
import { type FrontEndSettings } from "../models/frontend.settings"

export function useSettingsLoad(url: string) {
    const [settingsLoaded, setSettingsLoaded] = useState<boolean>(false)
    const [initialLoadSettings, setInitialLoadSettings] = useState<FrontEndSettings>({
        uninvested: 0.0,
        minimum: 0,
        ratings: ["Buy", "Strong Buy", "Hold", "N/A", "Underperform"],
        toOmit: []
    })

    useEffect(() => {
        axios.get<DefaultSettings>(url).then((response) => {
            setInitialLoadSettings({
                uninvested: response.data.personalData.uninvestedCash,
                minimum: response.data.personalData.minumumUnitsToBuy,
                ratings: response.data.personalData.ratingsTolerance,
                toOmit: response.data.tickersToOmit
            })
        }).catch((error) => {
            console.error(error)
        }).finally(() => {
            setSettingsLoaded(true)
        })
    }, [url])

    return { settingsLoaded, initialLoadSettings }
}