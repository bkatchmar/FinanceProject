import { create } from 'zustand'

export const useSettingsStore = create((set) => ({
    uninvested: 0.0,
    minimum: 0,
    ratings: ["Buy", "Strong Buy", "Hold", "N/A", "Underperform"],
    strategies: ["Cash Secured Put"],
    toOmit: [],
    setUninvested: (amount) => set({ uninvested: amount }),
    setMinimum: (amount) => set({ minimum: amount }),
    setRatings: (ratingSet) => set({ ratings: ratingSet }),
    setStrategies: (strats) => set({ strategies: strats }),
    setToOmit: (omissions) => set({ toOmit: omissions })
}))