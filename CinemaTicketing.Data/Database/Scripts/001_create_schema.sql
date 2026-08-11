SET FOREIGN_KEY_CHECKS = 0;

CREATE TABLE IF NOT EXISTS cinemas (
    id BIGINT NOT NULL AUTO_INCREMENT,
    name VARCHAR(150) NOT NULL,
    address VARCHAR(255) NOT NULL DEFAULT '',
    mobile VARCHAR(30) NOT NULL DEFAULT '',
    gstin VARCHAR(30) NOT NULL DEFAULT '',
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS users (
    id BIGINT NOT NULL AUTO_INCREMENT,
    user_name VARCHAR(50) NOT NULL,
    display_name VARCHAR(100) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    UNIQUE KEY uq_users_user_name (user_name)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS application_settings (
    setting_key VARCHAR(100) NOT NULL,
    setting_value VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL DEFAULT '',
    updated_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (setting_key)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS show_types (
    id BIGINT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    display_order INT NOT NULL,
    PRIMARY KEY (id),
    UNIQUE KEY uq_show_types_name (name)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tax_components (
    id BIGINT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    rate DECIMAL(10,2) NOT NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (id),
    UNIQUE KEY uq_tax_components_name (name)
) ENGINE=InnoDB;

ALTER TABLE cinemas MODIFY COLUMN id BIGINT NOT NULL AUTO_INCREMENT;
ALTER TABLE show_types MODIFY COLUMN id BIGINT NOT NULL AUTO_INCREMENT;

DROP TABLE IF EXISTS theatre_settings;

CREATE TABLE IF NOT EXISTS theatre_settings (
    id BIGINT NOT NULL AUTO_INCREMENT,
    cinema_id BIGINT NOT NULL,
    show_type_id BIGINT NOT NULL,
    show_time TIME NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (id),
    UNIQUE KEY uq_theatre_settings (cinema_id, show_type_id, show_time),
    KEY idx_theatre_settings_cinema (cinema_id),
    KEY idx_theatre_settings_show_type (show_type_id),
    CONSTRAINT fk_theatre_settings_cinema FOREIGN KEY (cinema_id) REFERENCES cinemas(id) ON DELETE CASCADE,
    CONSTRAINT fk_theatre_settings_show_type FOREIGN KEY (show_type_id) REFERENCES show_types(id) ON DELETE CASCADE
) ENGINE=InnoDB;

SET FOREIGN_KEY_CHECKS = 1;
