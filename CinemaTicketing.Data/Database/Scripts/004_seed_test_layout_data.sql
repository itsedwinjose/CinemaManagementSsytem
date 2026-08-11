-- Seed default Audis
INSERT IGNORE INTO audis (id, cinema_id, name, display_order, total_rows, total_cols) VALUES
(1, 1, 'Audi-1', 1, 10, 15),
(2, 1, 'Audi-2', 2, 10, 15),
(3, 1, 'Audi-3', 3, 12, 18),
(4, 1, 'Audi-4', 4, 10, 15);

-- Seed default seat classes
INSERT IGNORE INTO seat_classes (id, name, display_order, is_active) VALUES
(1, 'Normal', 1, 1),
(2, 'Executive', 2, 1),
(3, 'Premium', 3, 1);

-- Seed default movies
INSERT IGNORE INTO movies (id, name, is_3d, is_active) VALUES
(1, 'Avatar: The Way of Water', 1, 1),
(2, 'Oppenheimer', 0, 1),
(3, 'Jawan', 0, 1);

-- Generate default layout cells for Audi-1 if empty
INSERT IGNORE INTO audi_layout_cells (audi_id, row_index, col_index, is_seat, row_label, seat_number, seat_class_id)
SELECT 1, r.r, c.c, 1,
       CHAR(65 + r.r),
       CONCAT(CHAR(65 + r.r), c.c + 1),
       IF(r.r < 3, 3, IF(r.r < 7, 2, 1))
FROM (SELECT 0 AS r UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9) r
CROSS JOIN (SELECT 0 AS c UNION SELECT 1 UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8 UNION SELECT 9 UNION SELECT 10 UNION SELECT 11 UNION SELECT 12 UNION SELECT 13 UNION SELECT 14) c;
