import { type PersonalData } from "./personal.data"

export interface GetStrategiesRequest {
  tickersToOmit: string[]
  personalData: PersonalData
  includeCoverCalls: boolean
  includeCashSecuredPuts: boolean
}