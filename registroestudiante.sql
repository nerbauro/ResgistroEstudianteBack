-- phpMyAdmin SQL Dump
-- version 6.0.0-dev+20250905.4c34850c0b
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jan 19, 2026 at 12:27 AM
-- Server version: 8.4.3
-- PHP Version: 8.3.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `registroestudiante`
--

-- --------------------------------------------------------

--
-- Table structure for table `materia`
--

CREATE TABLE `materia` (
  `Mat_Id` int NOT NULL,
  `Mat_Codigo` varchar(15) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Mat_Descripcion` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Mat_Creditos` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `materia`
--

INSERT INTO `materia` (`Mat_Id`, `Mat_Codigo`, `Mat_Descripcion`, `Mat_Creditos`) VALUES
(1, 'CAL0001', 'Cálculo 1', 3),
(2, 'CAL000011', 'Cálculo 1', 3),
(3, 'CAL0002', 'Cálculo 2', 3);

-- --------------------------------------------------------

--
-- Table structure for table `materiaestudiante`
--

CREATE TABLE `materiaestudiante` (
  `MatEst_Id` int NOT NULL,
  `Mat_Id` int NOT NULL,
  `Usu_Id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `materiaestudiante`
--

INSERT INTO `materiaestudiante` (`MatEst_Id`, `Mat_Id`, `Usu_Id`) VALUES
(1, 1, 4),
(2, 3, 4),
(3, 1, 7),
(4, 3, 8),
(5, 2, 8);

-- --------------------------------------------------------

--
-- Table structure for table `materiaprofesor`
--

CREATE TABLE `materiaprofesor` (
  `MatPro_Id` int NOT NULL,
  `Mat_Id` int NOT NULL,
  `Usu_Id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `materiaprofesor`
--

INSERT INTO `materiaprofesor` (`MatPro_Id`, `Mat_Id`, `Usu_Id`) VALUES
(3, 1, 3),
(4, 2, 3),
(5, 3, 6);

-- --------------------------------------------------------

--
-- Table structure for table `permiso`
--

CREATE TABLE `permiso` (
  `Per_Id` int NOT NULL,
  `Per_Controlador` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Per_Pagina` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Rol_Id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `permiso`
--

INSERT INTO `permiso` (`Per_Id`, `Per_Controlador`, `Per_Pagina`, `Rol_Id`) VALUES
(1, 'Usuario', 'Crear', 1),
(2, 'Usuario', 'Editar', 1),
(3, 'Usuario', 'Consultar', 1),
(4, 'Usuario', 'ConsultarPorCodigo', 1),
(5, 'Materia', 'Consultar', 1),
(6, 'Materia', 'ConsultarPorCodigo', 1),
(7, 'Materia', 'Crear', 1),
(8, 'Materia', 'Editar', 1),
(9, 'MateriaProfesor', 'Crear', 1),
(10, 'MateriaEstudiante', 'Crear', 1),
(11, 'MateriaEstudiante', 'Crear', 3),
(12, 'MateriaEstudiante', 'ObtenerMateriaCompartida', 1),
(13, 'MateriaEstudiante', 'ObtenerMateriaCompartida', 3),
(14, 'MateriaEstudiante', 'ObtenerMateriaEstudianteConProfesor', 1),
(15, 'MateriaEstudiante', 'ObtenerMateriaEstudianteConProfesor', 3);

-- --------------------------------------------------------

--
-- Table structure for table `rol`
--

CREATE TABLE `rol` (
  `Rol_Id` int NOT NULL,
  `Rol_Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `rol`
--

INSERT INTO `rol` (`Rol_Id`, `Rol_Nombre`) VALUES
(1, 'Admin'),
(3, 'Estudiante'),
(2, 'Profesor');

-- --------------------------------------------------------

--
-- Table structure for table `usuario`
--

CREATE TABLE `usuario` (
  `Usu_Id` int NOT NULL,
  `Usu_Codigo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Usu_Nombre` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Usu_Clave` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Usu_Email` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Rol_Id` int NOT NULL,
  `Usu_Activo` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `usuario`
--

INSERT INTO `usuario` (`Usu_Id`, `Usu_Codigo`, `Usu_Nombre`, `Usu_Clave`, `Usu_Email`, `Rol_Id`, `Usu_Activo`) VALUES
(1, 'admin', 'Administrador', '$2a$11$s61MhLDBMS/5aA6cnNblFeX24V5X5m1JT29L3N3l6LtVIYHI0mOHy', 'admin@demo.com', 1, 1),
(3, '1090409011', 'Nestor Eduardo Bautista', '$2a$11$x88cGmDY0L4x2P1ISIKeV.3Z/5A8nzBNEW3I3IUNQW//hEio4Hi0W', 'nestorbauqgmail.com', 2, 1),
(4, '1090409012', 'Pedro Castro', '$2a$11$7in8bZACdBsWv6zNeiBsX.oReWxbDUnGORuZml3M3XYYQHHmGbQ6W', 'Pedro@gmail.com', 3, 1),
(5, '1090409013', 'Camila Suarez', '$2a$11$Y2GnUiqD/ZtzyXDx0wsybe3UV9Psw9awP.OrvXJRYfaoAoYaf4xG2', 'camila@gmail.com', 2, 1),
(6, '1090409014', 'Martha Ortiz', '$2a$11$iQRGNk2UIPYx15i0LmNXtuQddoMA/G8FH0XzDdp614kYY5dcMpjcC', 'martha@gmail.com', 2, 1),
(7, '1090409015', 'Karen Barrera', '$2a$11$7b/cFZfIWM/iOGsNu03iIuLUJcySnp1Tb56n81g.ehOrNIxKcxeS2', 'karen@gmail.com', 3, 1),
(8, '1090409016', 'Martin Orjuela', '$2a$11$K2gQXIcGgeMJfIIBzakQm.Zeor2Iy3za8hPAmqPkGzDLs5an77gOq', 'martin@gmail.com', 3, 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `materia`
--
ALTER TABLE `materia`
  ADD PRIMARY KEY (`Mat_Id`);

--
-- Indexes for table `materiaestudiante`
--
ALTER TABLE `materiaestudiante`
  ADD PRIMARY KEY (`MatEst_Id`),
  ADD KEY `Mat_Id` (`Mat_Id`),
  ADD KEY `Usu_Id` (`Usu_Id`);

--
-- Indexes for table `materiaprofesor`
--
ALTER TABLE `materiaprofesor`
  ADD PRIMARY KEY (`MatPro_Id`),
  ADD KEY `Mat_Id` (`Mat_Id`),
  ADD KEY `Usu_Id` (`Usu_Id`);

--
-- Indexes for table `permiso`
--
ALTER TABLE `permiso`
  ADD PRIMARY KEY (`Per_Id`),
  ADD KEY `permiso_ibfk_1` (`Rol_Id`);

--
-- Indexes for table `rol`
--
ALTER TABLE `rol`
  ADD PRIMARY KEY (`Rol_Id`),
  ADD UNIQUE KEY `Rol_Nombre` (`Rol_Nombre`);

--
-- Indexes for table `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`Usu_Id`),
  ADD UNIQUE KEY `Usu_Codigo` (`Usu_Codigo`),
  ADD KEY `usuario_ibfk_1` (`Rol_Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `materia`
--
ALTER TABLE `materia`
  MODIFY `Mat_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `materiaestudiante`
--
ALTER TABLE `materiaestudiante`
  MODIFY `MatEst_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `materiaprofesor`
--
ALTER TABLE `materiaprofesor`
  MODIFY `MatPro_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `permiso`
--
ALTER TABLE `permiso`
  MODIFY `Per_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `rol`
--
ALTER TABLE `rol`
  MODIFY `Rol_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `usuario`
--
ALTER TABLE `usuario`
  MODIFY `Usu_Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `materiaestudiante`
--
ALTER TABLE `materiaestudiante`
  ADD CONSTRAINT `materiaestudiante_ibfk_1` FOREIGN KEY (`Mat_Id`) REFERENCES `materia` (`Mat_Id`),
  ADD CONSTRAINT `materiaestudiante_ibfk_2` FOREIGN KEY (`Usu_Id`) REFERENCES `usuario` (`Usu_Id`);

--
-- Constraints for table `materiaprofesor`
--
ALTER TABLE `materiaprofesor`
  ADD CONSTRAINT `materiaprofesor_ibfk_1` FOREIGN KEY (`Mat_Id`) REFERENCES `materia` (`Mat_Id`),
  ADD CONSTRAINT `materiaprofesor_ibfk_2` FOREIGN KEY (`Usu_Id`) REFERENCES `usuario` (`Usu_Id`);

--
-- Constraints for table `permiso`
--
ALTER TABLE `permiso`
  ADD CONSTRAINT `permiso_ibfk_1` FOREIGN KEY (`Rol_Id`) REFERENCES `rol` (`Rol_Id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Constraints for table `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`Rol_Id`) REFERENCES `rol` (`Rol_Id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
