# Use a imagem oficial do RabbitMQ
FROM rabbitmq:3-management-alpine

# plugins RabbitMQ
COPY docker-conf/rabbitmq/config/enabled_plugins /etc/rabbitmq/enabled_plugins
# configs RabbitMQ (Backend Auth)
COPY docker-conf/rabbitmq/config/rabbitmq.conf /etc/rabbitmq/rabbitmq.conf

# porta MQTT padrão do RabbitMQ
EXPOSE 1883 8883 15675 15674

# Inicie o RabbitMQ
CMD ["rabbitmq-server"]

