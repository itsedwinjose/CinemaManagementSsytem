SET FOREIGN_KEY_CHECKS = 0;

CREATE TABLE IF NOT EXISTS audis (
    id BIGINT NOT NULL AUTO_INCREMENT,
    cinema_id BIGINT NOT NULL,
    name VARCHAR(50) NOT NULL,
    display_order INT NOT NULL DEFAULT 1,
    total_rows INT NOT NULL DEFAULT 10,
    total_cols INT NOT NULL DEFAULT 15,
    created_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY idx_audis_cinema (cinema_id),
    CONSTRAINT fk_audis_cinema FOREIGN KEY (cinema_id) REFERENCES cinemas(id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS seat_classes (
    id BIGINT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    display_order INT NOT NULL DEFAULT 1,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (id),
    UNIQUE KEY uq_seat_classes_name (name)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS audi_layout_cells (
    id BIGINT NOT NULL AUTO_INCREMENT,
    audi_id BIGINT NOT NULL,
    row_index INT NOT NULL,
    col_index INT NOT NULL,
    is_seat TINYINT(1) NOT NULL DEFAULT 1,
    row_label VARCHAR(10) NOT NULL DEFAULT '',
    seat_number VARCHAR(10) NOT NULL DEFAULT '',
    seat_class_id BIGINT NULL,
    is_damaged TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (id),
    UNIQUE KEY uq_audi_layout_cell (audi_id, row_index, col_index),
    KEY idx_cells_class (seat_class_id),
    CONSTRAINT fk_cells_audi FOREIGN KEY (audi_id) REFERENCES audis(id) ON DELETE CASCADE,
    CONSTRAINT fk_cells_class FOREIGN KEY (seat_class_id) REFERENCES seat_classes(id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS show_class_prices (
    id BIGINT NOT NULL AUTO_INCREMENT,
    theatre_setting_id BIGINT NOT NULL,
    seat_class_id BIGINT NOT NULL,
    price DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (id),
    UNIQUE KEY uq_show_class_price (theatre_setting_id, seat_class_id),
    KEY idx_scp_class (seat_class_id),
    CONSTRAINT fk_scp_setting FOREIGN KEY (theatre_setting_id) REFERENCES theatre_settings(id) ON DELETE CASCADE,
    CONSTRAINT fk_scp_class FOREIGN KEY (seat_class_id) REFERENCES seat_classes(id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS movies (
    id BIGINT NOT NULL AUTO_INCREMENT,
    name VARCHAR(150) NOT NULL,
    is_3d TINYINT(1) NOT NULL DEFAULT 0,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    PRIMARY KEY (id),
    UNIQUE KEY uq_movies_name (name)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS screenings (
    id BIGINT NOT NULL AUTO_INCREMENT,
    cinema_id BIGINT NOT NULL,
    audi_id BIGINT NOT NULL,
    show_type_id BIGINT NOT NULL,
    show_time TIME NOT NULL,
    screening_date DATE NOT NULL,
    movie_id BIGINT NULL,
    is_current_show TINYINT(1) NOT NULL DEFAULT 0,
    PRIMARY KEY (id),
    UNIQUE KEY uq_screenings (cinema_id, audi_id, screening_date, show_type_id, show_time),
    KEY idx_scr_audi (audi_id),
    KEY idx_scr_show_type (show_type_id),
    KEY idx_scr_movie (movie_id),
    CONSTRAINT fk_scr_cinema FOREIGN KEY (cinema_id) REFERENCES cinemas(id),
    CONSTRAINT fk_scr_audi FOREIGN KEY (audi_id) REFERENCES audis(id),
    CONSTRAINT fk_scr_show_type FOREIGN KEY (show_type_id) REFERENCES show_types(id),
    CONSTRAINT fk_scr_movie FOREIGN KEY (movie_id) REFERENCES movies(id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS screening_seats (
    id BIGINT NOT NULL AUTO_INCREMENT,
    screening_id BIGINT NOT NULL,
    audi_layout_cell_id BIGINT NOT NULL,
    status VARCHAR(30) NOT NULL DEFAULT 'AVAILABLE',
    PRIMARY KEY (id),
    UNIQUE KEY uq_screening_seat (screening_id, audi_layout_cell_id),
    KEY idx_ss_cell (audi_layout_cell_id),
    CONSTRAINT fk_ss_screening FOREIGN KEY (screening_id) REFERENCES screenings(id) ON DELETE CASCADE,
    CONSTRAINT fk_ss_cell FOREIGN KEY (audi_layout_cell_id) REFERENCES audi_layout_cells(id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS bookings (
    id BIGINT NOT NULL AUTO_INCREMENT,
    screening_id BIGINT NOT NULL,
    user_id BIGINT NOT NULL,
    booking_number VARCHAR(50) NOT NULL,
    booking_type VARCHAR(30) NOT NULL,
    payment_mode VARCHAR(20) NOT NULL,
    customer_name VARCHAR(100) NOT NULL DEFAULT '',
    customer_phone VARCHAR(30) NOT NULL DEFAULT '',
    customer_address VARCHAR(255) NOT NULL DEFAULT '',
    ticket_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    reservation_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    three_d_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    tax_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    created_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    UNIQUE KEY uq_booking_number (booking_number),
    KEY idx_bk_screening (screening_id),
    KEY idx_bk_user (user_id),
    CONSTRAINT fk_bk_screening FOREIGN KEY (screening_id) REFERENCES screenings(id),
    CONSTRAINT fk_bk_user FOREIGN KEY (user_id) REFERENCES users(id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS booking_seats (
    id BIGINT NOT NULL AUTO_INCREMENT,
    booking_id BIGINT NOT NULL,
    screening_seat_id BIGINT NOT NULL,
    seat_class_name VARCHAR(50) NOT NULL DEFAULT '',
    seat_number VARCHAR(20) NOT NULL DEFAULT '',
    price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    PRIMARY KEY (id),
    KEY idx_bs_booking (booking_id),
    KEY idx_bs_screening_seat (screening_seat_id),
    CONSTRAINT fk_bs_booking FOREIGN KEY (booking_id) REFERENCES bookings(id) ON DELETE CASCADE,
    CONSTRAINT fk_bs_screening_seat FOREIGN KEY (screening_seat_id) REFERENCES screening_seats(id)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS payments (
    id BIGINT NOT NULL AUTO_INCREMENT,
    booking_id BIGINT NOT NULL,
    payment_mode VARCHAR(20) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    created_utc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    KEY idx_pmt_booking (booking_id),
    CONSTRAINT fk_pmt_booking FOREIGN KEY (booking_id) REFERENCES bookings(id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS cashout_records (
    id BIGINT NOT NULL AUTO_INCREMENT,
    cinema_id BIGINT NOT NULL,
    show_date DATE NOT NULL,
    show_time TIME NOT NULL,
    show_type_name VARCHAR(50) NOT NULL,
    movie_name VARCHAR(150) NOT NULL DEFAULT '',
    sold_seats INT NOT NULL DEFAULT 0,
    free_seats INT NOT NULL DEFAULT 0,
    reserved_seats INT NOT NULL DEFAULT 0,
    ticket_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    reservation_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    three_d_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    cash_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    card_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    online_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    upi_amount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    cashed_out_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    cashed_out_by VARCHAR(100) NOT NULL DEFAULT '',
    is_printed TINYINT(1) NOT NULL DEFAULT 0,
    printed_at DATETIME NULL,
    printed_by VARCHAR(100) NULL,
    PRIMARY KEY (id),
    KEY idx_co_cinema (cinema_id),
    CONSTRAINT fk_co_cinema FOREIGN KEY (cinema_id) REFERENCES cinemas(id)
) ENGINE=InnoDB;

SET FOREIGN_KEY_CHECKS = 1;
