namespace UMMO.TestingUtils.RandomData
{
    public interface IRandomString
    {
        /// <summary>
        /// A random first name
        /// </summary>
        /// <value>The first name.</value>
        string FirstName { get; }

        /// <summary>
        /// A random last name.
        /// </summary>
        /// <value>The last name.</value>
        string LastName { get; }

        /// <summary>
        /// A random password.
        /// </summary>
        /// <value>The password.</value>
        string Password { get; }

        /// <summary>
        /// A random noun.
        /// </summary>
        /// <value>The noun.</value>
        string Noun { get; }

        /// <summary>
        /// A random verb.
        /// </summary>
        /// <value>The verb.</value>
        string Verb { get; }
    }
}