export interface SettingsState {
    uninvested: number
    minimum: number
    ratings: string[]
    strategies: string[]
    toOmit: string[]
    setUninvested: (amount: number) => void
    setMinimum: (amount: number) => void
    setRatings: (ratingSet: string[]) => void
    setStrategies: (strats: string[]) => void
    setToOmit: (omissions: string[]) => void
}