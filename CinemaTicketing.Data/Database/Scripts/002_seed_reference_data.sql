INSERT IGNORE INTO cinemas (id, name, address, mobile, gstin, is_active)
VALUES (1, 'Demo Cinema', '', '', '', 1);

INSERT IGNORE INTO application_settings (setting_key, setting_value, description)
VALUES
    ('reservation_charge', '10', 'Default reservation charge per seat'),
    ('three_d_charge', '30', 'Default optional 3D charge per seat'),
    ('currency_symbol', 'Rs.', 'Display currency symbol');

INSERT IGNORE INTO show_types (name, display_order)
VALUES
    ('Morning', 1),
    ('Matinee', 2),
    ('First Show', 3),
    ('Second Show', 4);

INSERT IGNORE INTO tax_components (name, rate, is_active)
VALUES
    ('GST', 0.00, 1);

INSERT IGNORE INTO users (user_name, display_name, password_hash, is_active)
VALUES
    ('admin', 'Administrator', '100000.v3wT1Y5N+Xh9Z/7K+mJ3Lw==.1111111111111111111111111111111111111111111=', 1);
