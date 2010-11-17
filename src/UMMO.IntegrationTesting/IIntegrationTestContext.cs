using System.Data;

namespace UMMO.IntegrationTesting
{
    public interface IIntegrationTestContext
    {
        IDbConnection DatabaseConnection { get; }

        IDbTransaction CurrentTransaction { get; }
    }
}