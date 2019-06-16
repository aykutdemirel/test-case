using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Config
{
    public class RabbitMQConfig
    {
        public static string RabbitMQUri = "rabbitmq://rabbitmq:5672/";
        public static string RabbitMQUserName = "guest";
        public static string RabbitMQPassword = "guest";
    }
}
