namespace DDDInvoicingCleanL.SharedKernel
{
    public static class MessagingConstants
    {
        public static class Credentials
        {
            public const string DEFAULT_USERNAME = "guest";
            public const string DEFAULT_PASSWORD = "guest";
        }
        public static class Exchanges
        {
            public const string DDDCLEANARCHSTARTER_BUSINESSMANAGEMENT_EXCHANGE = "dddcleanarchstarter-queueservice-2-mq";
            public const string DDDCLEANARCHSTARTER_RABBITMQMONGOWATCHER_EXCHANGE =
                "dddcleanarchstarter-rabbitmqmongowatcher";
        }
        public static class NetworkConfig
        {
            public const int DEFAULT_PORT = 5672;
            public const string DEFAULT_VIRTUAL_HOST = "/";
        }
        public static class Queues
        {
            public const string FDCM_BUSINESSMANAGEMENT_IN = "fdcm-businessmanagement-in";
            public const string FDCM_DDDCLEANARCHSTARTER_IN = "fdcm-dddcleanarchstarter-in";
            public const string FDVCP_DDDCLEANARCHSTARTER_IN = "fdvcp-dddcleanarchstarter-in";
            public const string FDVCP_RABBITMQMONGOWATCHER_IN = "fdvcp-rabbitmqmongowatcher-in";
        }
    }
}