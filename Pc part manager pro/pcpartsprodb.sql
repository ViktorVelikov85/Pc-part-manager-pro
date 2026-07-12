-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 12, 2026 at 11:57 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pcpartsprodb`
--

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`id`, `name`) VALUES
(1, 'CPU'),
(2, 'GPU'),
(4, 'Motherboard'),
(3, 'RAM'),
(5, 'SSD');

-- --------------------------------------------------------

--
-- Table structure for table `computer_parts`
--

CREATE TABLE `computer_parts` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `manufacturer` varchar(50) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `quantity` int(11) NOT NULL DEFAULT 0,
  `category_id` int(11) NOT NULL,
  `part_type` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `computer_parts`
--

INSERT INTO `computer_parts` (`id`, `name`, `manufacturer`, `price`, `quantity`, `category_id`, `part_type`) VALUES
(1, 'Ryzen 7 7800X3D', 'AMD', 820.00, 8, 1, 'CPU'),
(2, 'GeForce RTX 4070 Ti Super', 'NVIDIA', 1850.00, 5, 2, 'GPU'),
(3, 'Vengeance RGB', 'Corsair', 240.00, 15, 3, 'RAM'),
(4, 'ROG STRIX B650-A GAMING', 'ASUS', 460.00, 4, 4, 'Motherboard'),
(5, '990 PRO M.2', 'Samsung', 280.00, 25, 5, 'SSD'),
(6, 'Ryzen 5 7600', 'AMD', 430.00, 12, 1, 'CPU'),
(7, 'Ryzen 7 9700X', 'AMD', 720.00, 7, 1, 'CPU'),
(8, 'Ryzen 9 9950X', 'AMD', 1390.00, 3, 1, 'CPU'),
(9, 'Core i5-14600K', 'Intel', 650.00, 10, 1, 'CPU'),
(10, 'Core i7-14700K', 'Intel', 920.00, 6, 1, 'CPU'),
(11, 'GeForce RTX 4060', 'NVIDIA', 720.00, 15, 2, 'GPU'),
(13, 'GeForce RTX 4080 SUPER', 'NVIDIA', 2490.00, 4, 2, 'GPU'),
(14, 'Radeon RX 7800 XT', 'AMD', 1190.00, 7, 2, 'GPU'),
(15, 'Radeon RX 7900 XTX', 'AMD', 2190.00, 3, 2, 'GPU'),
(16, 'Kingston Fury Beast DDR5 32GB', 'Kingston', 235.00, 18, 3, 'RAM'),
(17, 'G.Skill Trident Z5 RGB 32GB', 'G.Skill', 295.00, 10, 3, 'RAM'),
(18, 'Corsair Vengeance DDR5 64GB', 'Corsair', 520.00, 6, 3, 'RAM'),
(19, 'Kingston Fury Beast DDR4 32GB', 'Kingston', 175.00, 14, 3, 'RAM'),
(20, 'Crucial Pro DDR5 32GB', 'Crucial', 210.00, 12, 3, 'RAM'),
(21, 'MSI MAG B650 Tomahawk WiFi', 'MSI', 445.00, 6, 4, 'Motherboard'),
(22, 'Gigabyte B650 AORUS Elite AX', 'Gigabyte', 470.00, 5, 4, 'Motherboard'),
(23, 'ASUS TUF GAMING B760-PLUS WIFI', 'ASUS', 430.00, 7, 4, 'Motherboard'),
(24, 'MSI PRO Z790-P WIFI', 'MSI', 560.00, 4, 4, 'Motherboard'),
(25, 'ASRock B650M Pro RS', 'ASRock', 315.00, 8, 4, 'Motherboard'),
(26, 'WD Black SN850X 1TB', 'Western Digital', 250.00, 20, 5, 'SSD'),
(27, 'Kingston KC3000 2TB', 'Kingston', 390.00, 12, 5, 'SSD'),
(28, 'Crucial T500 1TB', 'Crucial', 225.00, 14, 5, 'SSD'),
(29, 'Samsung 980 PRO 2TB', 'Samsung', 430.00, 9, 5, 'SSD'),
(30, 'Lexar NM790 2TB', 'Lexar', 315.00, 11, 5, 'SSD');

-- --------------------------------------------------------

--
-- Table structure for table `part_cpus`
--

CREATE TABLE `part_cpus` (
  `part_id` int(11) NOT NULL,
  `cores` int(11) NOT NULL,
  `socket` varchar(20) NOT NULL,
  `clock_ghz` decimal(4,2) NOT NULL,
  `tdp_watts` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `part_cpus`
--

INSERT INTO `part_cpus` (`part_id`, `cores`, `socket`, `clock_ghz`, `tdp_watts`) VALUES
(1, 8, 'AM5', 4.20, 120),
(6, 6, 'AM5', 3.80, 65),
(7, 8, 'AM5', 3.80, 65),
(8, 16, 'AM5', 4.30, 170),
(9, 14, 'LGA1700', 3.50, 125),
(10, 20, 'LGA1700', 3.40, 125);

-- --------------------------------------------------------

--
-- Table structure for table `part_gpus`
--

CREATE TABLE `part_gpus` (
  `part_id` int(11) NOT NULL,
  `vram_size_gb` int(11) NOT NULL,
  `memory_type` varchar(20) NOT NULL,
  `clock_mhz` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `part_gpus`
--

INSERT INTO `part_gpus` (`part_id`, `vram_size_gb`, `memory_type`, `clock_mhz`) VALUES
(2, 16, 'GDDR6X', 2610),
(11, 8, 'GDDR6', 2460),
(13, 16, 'GDDR6X', 2550),
(14, 16, 'GDDR6', 2430),
(15, 24, 'GDDR6', 2500);

-- --------------------------------------------------------

--
-- Table structure for table `part_motherboards`
--

CREATE TABLE `part_motherboards` (
  `part_id` int(11) NOT NULL,
  `chipset` varchar(20) NOT NULL,
  `form_factor` varchar(20) NOT NULL,
  `socket` varchar(20) NOT NULL,
  `supported_ram_gen` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `part_motherboards`
--

INSERT INTO `part_motherboards` (`part_id`, `chipset`, `form_factor`, `socket`, `supported_ram_gen`) VALUES
(4, 'B650', 'ATX', 'AM5', 'DDR5'),
(21, 'B650', 'ATX', 'AM5', 'DDR5'),
(22, 'B650', 'ATX', 'AM5', 'DDR5'),
(23, 'B760', 'ATX', 'LGA1700', 'DDR5'),
(24, 'Z790', 'ATX', 'LGA1700', 'DDR5'),
(25, 'B650', 'Micro-ATX', 'AM5', 'DDR5');

-- --------------------------------------------------------

--
-- Table structure for table `part_rams`
--

CREATE TABLE `part_rams` (
  `part_id` int(11) NOT NULL,
  `capacity_gb` int(11) NOT NULL,
  `speed_mhz` int(11) NOT NULL,
  `generation` varchar(10) NOT NULL,
  `cas_latency` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `part_rams`
--

INSERT INTO `part_rams` (`part_id`, `capacity_gb`, `speed_mhz`, `generation`, `cas_latency`) VALUES
(3, 32, 6000, 'DDR5', 'CL30'),
(16, 32, 6000, 'DDR5', 'CL36'),
(17, 32, 6400, 'DDR5', 'CL32'),
(18, 64, 6000, 'DDR5', 'CL30'),
(19, 32, 3600, 'DDR4', 'CL18'),
(20, 32, 6000, 'DDR5', 'CL36');

-- --------------------------------------------------------

--
-- Table structure for table `part_ssds`
--

CREATE TABLE `part_ssds` (
  `part_id` int(11) NOT NULL,
  `capacity_gb` int(11) NOT NULL,
  `form_factor` varchar(20) NOT NULL,
  `read_speed_mb` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `part_ssds`
--

INSERT INTO `part_ssds` (`part_id`, `capacity_gb`, `form_factor`, `read_speed_mb`) VALUES
(5, 1000, 'M.2 NVMe', 7450),
(26, 1000, 'M.2 NVMe', 7300),
(27, 2000, 'M.2 NVMe', 7000),
(28, 1000, 'M.2 NVMe', 7400),
(29, 2000, 'M.2 NVMe', 7000),
(30, 2000, 'M.2 NVMe', 7400);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `computer_parts`
--
ALTER TABLE `computer_parts`
  ADD PRIMARY KEY (`id`),
  ADD KEY `category_id` (`category_id`);

--
-- Indexes for table `part_cpus`
--
ALTER TABLE `part_cpus`
  ADD PRIMARY KEY (`part_id`);

--
-- Indexes for table `part_gpus`
--
ALTER TABLE `part_gpus`
  ADD PRIMARY KEY (`part_id`);

--
-- Indexes for table `part_motherboards`
--
ALTER TABLE `part_motherboards`
  ADD PRIMARY KEY (`part_id`);

--
-- Indexes for table `part_rams`
--
ALTER TABLE `part_rams`
  ADD PRIMARY KEY (`part_id`);

--
-- Indexes for table `part_ssds`
--
ALTER TABLE `part_ssds`
  ADD PRIMARY KEY (`part_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `computer_parts`
--
ALTER TABLE `computer_parts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `computer_parts`
--
ALTER TABLE `computer_parts`
  ADD CONSTRAINT `computer_parts_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`);

--
-- Constraints for table `part_cpus`
--
ALTER TABLE `part_cpus`
  ADD CONSTRAINT `part_cpus_ibfk_1` FOREIGN KEY (`part_id`) REFERENCES `computer_parts` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `part_gpus`
--
ALTER TABLE `part_gpus`
  ADD CONSTRAINT `part_gpus_ibfk_1` FOREIGN KEY (`part_id`) REFERENCES `computer_parts` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `part_motherboards`
--
ALTER TABLE `part_motherboards`
  ADD CONSTRAINT `part_motherboards_ibfk_1` FOREIGN KEY (`part_id`) REFERENCES `computer_parts` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `part_rams`
--
ALTER TABLE `part_rams`
  ADD CONSTRAINT `part_rams_ibfk_1` FOREIGN KEY (`part_id`) REFERENCES `computer_parts` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `part_ssds`
--
ALTER TABLE `part_ssds`
  ADD CONSTRAINT `part_ssds_ibfk_1` FOREIGN KEY (`part_id`) REFERENCES `computer_parts` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
