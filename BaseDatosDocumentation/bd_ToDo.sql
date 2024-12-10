--
-- PostgreSQL database cluster dump
--

-- Started on 2024-12-09 11:35:21

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE administrador;
ALTER ROLE administrador WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN NOREPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:mkaV1WMPMxnIIUsg5yknFQ==$H0wGj5dIVp9D5kETIxVaTM3mGFKo+98kNeJT+7dJPcw=:BRFLzhc/1JAx8X0UjxhKa7UTCrF+DuUIx13YtQpn0S0=';
CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'SCRAM-SHA-256$4096:r/RDZ4uLEBfc3lvxhr7SzQ==$wt5pu3LYqPACkCWpA78ipgtzoNbOS+GzMMscZqk/2t4=:PwaDMbpYGdk6jpZU6aE00WngseKu2QW0DkslRazMUhw=';

--
-- User Configurations
--








-- Completed on 2024-12-09 11:35:21

--
-- PostgreSQL database cluster dump complete
--

