FROM quay.io/keycloak/keycloak:latest as builder

ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

ENV KC_DB=postgres

WORKDIR /opt/keycloak

RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
RUN /opt/keycloak/bin/kc.sh build

FROM quay.io/keycloak/keycloak:latest
COPY --from=builder /opt/keycloak/ /opt/keycloak/

EXPOSE 8080 8443

# database
ENV KC_DB=postgres
ENV KC_DB_URL=jdbc:postgresql://postgres-db:5432/keycloak
ENV KC_DB_USERNAME=keycloak
ENV KC_DB_PASSWORD=admin

# login
ENV KEYCLOAK_ADMIN=admin
ENV KEYCLOAK_ADMIN_PASSWORD=admin
ENV KC_HOSTNAME=localhost
ENV KC_HOSTNAME_ADMIN_URL=http://localhost:8080

# security
ENV KC_HOSTNAME_STRICT=false
ENV KC_HOSTNAME_STRICT_HTTPS=false
ENV KC_HTTP_ENABLED=true

# cloud-infrastructure
#ENV KC_PROXY=edge
#ENV PROXY_ADDRESS_FORWARDING=true

ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]

CMD ["start"]
