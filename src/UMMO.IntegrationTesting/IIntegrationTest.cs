namespace UMMO.IntegrationTesting
{
    /// <summary>
    /// Interface which all integration tests should implement
    /// </summary>
    public interface IIntegrationTest
    {
        /// <summary>
        /// Setup routine for the integration test
        /// </summary>
        /// <param name="context">The context.</param>
        void Setup( IIntegrationTestContext context );

        /// <summary>
        /// Runs the integration test.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>True if the test passes, false otherwise</returns>
        bool Run( IIntegrationTestContext context );

        /// <summary>
        /// Cleans up after the test.
        /// </summary>
        /// <param name="context">The context.</param>
        void Cleanup( IIntegrationTestContext context );
    }
}