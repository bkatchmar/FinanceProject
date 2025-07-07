import { type PersonalData } from "./personal.data"

export interface DefaultSettings {
  weeklyCsvFileDownload: string
  personalData: PersonalData
  tickersToOmit: string[]
  fileNameToReadFrom: string
  fileToWriteTo: string
  nextMovesFile: string
}