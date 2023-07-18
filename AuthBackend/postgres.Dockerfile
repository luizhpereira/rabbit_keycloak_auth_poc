FROM postgres:latest

ENV POSTGRES_USER keycloak
ENV POSTGRES_PASSWORD cazbila
ENV POSTGRES_DB keycloak

EXPOSE 5432

ENTRYPOINT ["docker-entrypoint.sh"]

CMD ["postgres"]