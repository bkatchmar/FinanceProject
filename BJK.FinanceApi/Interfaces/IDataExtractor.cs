﻿namespace BJK.FinanceApi.Interfaces
{
    public interface IDataExtractor
    {
        IEnumerable<string> Symbols { get; }
        IEnumerable<IFinanceInstrument> InstrumentsInformation { get; }
        Task GetInformation();
        bool DoesInformationForFinancialInstrumentExists(string Symbol);
        IFinanceInstrument? GetFinanceInstrument(string Symbol);
    }
}