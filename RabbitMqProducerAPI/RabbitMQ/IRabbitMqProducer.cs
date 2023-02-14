namespace RabbitMqProducerAPI.RabbitMQ
{
    public interface IRabbitMqProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
