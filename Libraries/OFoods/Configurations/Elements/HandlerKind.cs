namespace OFoods.Configurations.Elements
{
    public enum HandlerSourceType
    {

        /// <summary>
        /// Type.
        /// </summary>
        Type,

        /// <summary>
        /// Assembly.
        /// </summary>
        Assembly,
    }
    public enum HandlerKind
    {

        /// <summary>
        /// Command.
        /// </summary>
        Command,

        /// <summary>
        /// Event.
        /// </summary>
        Event,
    }
    public enum ExceptionHandlingBehavior
    {

        /// <summary>
        /// Direct.
        /// </summary>
        Direct,

        /// <summary>
        /// Forward.
        /// </summary>
        Forward,
    }
}
