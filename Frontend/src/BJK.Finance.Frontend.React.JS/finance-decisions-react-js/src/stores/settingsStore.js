import { create } from 'zustand';

export const useSettingsStore = create((set) => ({
    uninvested: 0.0,
    minimum: 0,
    setUninvested: (amount) => set({ uninvested: amount }),
    setMinimum: (amount) => set({ minimum: amount })
}))