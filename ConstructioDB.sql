CREATE TABLE departments (
	department_id int AUTO_INCREMENT,
    name varchar(50) NOT NULL ,
    PRIMARY KEY(department_id)
);

CREATE TABLE locations (
	id char(36),
    name varchar(255) NOT NULL ,
    PRIMARY KEY(id)
);

CREATE TABLE equipments (
    id char(36),
    name varchar(50) NOT NULL ,
    PRIMARY KEY(id)
);

CREATE TABLE skills
/*does  the skills table need a project_id*/
	skill_id int AUTO_INCREMENT,/*do we need to auto increment this? */
    name varchar(50) NOT NULL ,
    PRIMARY KEY (skill_id)
	project_id char(36),
    skill_id int,
    
	CONSTRAINT fk_projects_skills_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_projects_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE projects (
	id char(36),
    expenses decimal(10,2),
    location_id char(36),
    PRIMARY KEY(id),
    CONSTRAINT fk_projects_location_id_locations FOREIGN KEY (location_id) REFERENCES locations(id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE employees (
    id char(36),
    name varchar (30) NOT NULL ,
    title varchar (50),
    payroll decimal(10,2),
    department_id int,
    project_id char(36),
    PRIMARY KEY(id),
    CONSTRAINT fk_employees_department_id_departments FOREIGN KEY (department_id) REFERENCES departments(id) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT fk_employees_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE SET NULL ON UPDATE CASCADE
);


CREATE TABLE employees_skills (
	employee_id char(36),
    skill_id int,
	
    PRIMARY KEY (employee_id, skill_id),
    CONSTRAINT fk_employees_skills_employee_id_employees FOREIGN KEY (employee_id) REFERENCES employees(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_employees_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE projects_skills (
	project_id char(36),
    skill_id int,
    PRIMARY KEY (project_id, skill_id),
    CONSTRAINT fk_projects_skills_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_projects_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(id) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE projects_equipments (
	project_id char(36),
    equipment_id char(36),
    PRIMARY KEY (project_id, equipment_id),
    CONSTRAINT fk_projects_equipments_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_projects_equipments_equipment_id_equipments FOREIGN KEY (equipment_id) REFERENCES equipments(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE projects_equipments (


);

CREATE TABLE resources(

PRIMARY KEY resource_id char(36),
FOREIGN KEY project_id INT,
FOREIGN KEY location_id,
resource_type char(20),
 CONSTRAINT fk_reources_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(id) ON DELETE CASCADE ON UPDATE CASCADE,
 CONSTRAINT fk_resources_locations_id_projects FOREIGN KEY (locations_id) REFERENCES locations(id) ON DELETE CASCADE ON UPDATE CASCADE

);


 


INSERT INTO departments (name)
VALUES
	('Operations'),
	('Marketing'),
	('Human Resources'),
	('Legal'),
	('Finance'),
    ('Construction');

INSERT INTO skills (name)
VALUES
	('Carpentry'),
	('Stonework'),
	('Tiling'),
	('Metalwork'),
	('Welding'),
	('Electrical Wiring'),
	('Demolition'),
	('Pipe fitting'),
	('Surveying'),
    ('Framing');

INSERT INTO equipments
VALUES
	('B3D64330-EB88-B81B-CE66-A63C079EDA93', 'Excavator'),
	('388E25C7-9F49-6C18-EAEB-E20B66B5766C', 'Grader'),
	('D0E5D69F-0111-EBE6-97EE-473247B8306A', 'Forklift'),
	('D164621A-4876-5DCC-2E33-4440A7394308', 'Backhoe'),
	('0B989C33-4F56-777D-BDB9-235B4B47ABBC', 'Paver'),
	('144946CE-1D19-9860-D971-4235C32232BB', 'Telehandlers'),
	('A32E71E5-D26B-6157-5E5C-9CA52B63DA62', 'Bulldozer'),
	('9D2F1EEE-E366-FB28-CA2A-5AA0DC7560B7', 'Dump Trucks'),
	('5CED26B2-4D73-6D5A-0755-24BCB819EE05', 'Rollers'),
	('1A87C254-5B7C-DEC5-33EA-73D9ACD8E127', 'Trencher'),
	('9D452D3B-0E7D-38D7-8BC1-BB1CF4D81A63', 'Manlift'),
	('61D3D1F2-1E4E-AEE8-25E8-6BEBFEA467BF', 'Compactor'),
    ('995DA342-AC81-12CF-B103-4317014B2FD1', 'Crane');

INSERT INTO locations (id,name)
VALUES
  ('6C4C22D5-D4BB-A213-C1E9-6D831B94599F', '335-2805 Eget, Av.'),
  ('F43F8A24-51D6-DB5C-BD33-CAAFD6BB6A16', 'Ap #370-2939 Nisl. St.'),
  ('DAA1D6F9-6147-569B-7ED2-85283DA8BF13', '320-7900 Neque Avenue'),
  ('71BCA4D8-E53C-5433-8855-1EB4C9E2C725', 'P.O. Box 352, 7908 Adipiscing Rd.'),
  ('66CD94D5-9D5A-6E16-C133-96A2D86CCCDA', '2537 Pede. Rd.'),
  ('18D736AB-D467-1E7B-BE01-136C86DD53DF', 'Ap #841-9585 Massa. St.'),
  ('2E21EBEA-DF8A-E35B-211A-8817BF3C6156', 'P.O. Box 539, 5425 Sed Ave'),
  ('53571310-F61E-2CDA-4042-F2AA7D021A37', '315-3749 Non St.'),
  ('8D7D7CD2-8681-DD58-69DE-CB842F850AEC', 'Ap #826-7536 Semper, Rd.'),
  ('DCB85C93-9CE3-B74D-AE95-F6B8197A1051', 'P.O. Box 576, 2332 Iaculis St.'),
  ('696A333D-DD17-522C-801F-67324037A32B', 'P.O. Box 394, 6043 Porttitor St.'),
  ('A08D6F7B-2D6A-2C77-7166-28A2A2495D82', 'Ap #290-7197 Integer Ave'),
  ('C1DAAA63-60B7-D2B6-5065-DBA6CD2D251A', '677-8766 Aliquam Av.'),
  ('2590D3B8-CAC6-63A4-47C9-B14C83BC3C3E', 'Ap #417-3994 Nullam Road'),
  ('DFE7890A-27FC-1976-0C6A-276E1D362A21', 'Ap #989-6009 Morbi Road'),
  ('A943C858-298A-35CA-5DDA-3BC030A52275', '4792 Vulputate St.'),
  ('BBE75196-A13A-F63C-5F31-768B4352A8D6', '225-1614 Aliquam Rd.'),
  ('C8895712-165D-75B1-95D8-59C06D3E52CA', 'Ap #693-173 Eleifend, St.'),
  ('3C074137-9991-5C94-3C2A-5B9AAC2CF5E6', '922-738 Dapibus Rd.'),
  ('2362CC29-5DFD-A69C-1AE4-9F14ED3D11C2', '1233 Dictum Ave');


INSERT INTO projects (id, expenses, location_id)
VALUES
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 12345.79, '6C4C22D5-D4BB-A213-C1E9-6D831B94599F'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 422926.96, 'F43F8A24-51D6-DB5C-BD33-CAAFD6BB6A16'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 253840.05, 'DAA1D6F9-6147-569B-7ED2-85283DA8BF13'),
	('c1c10b90-99a7-452f-9578-e4459efcefc1', 360506.99, '71BCA4D8-E53C-5433-8855-1EB4C9E2C725'),
	('f500f0a3-f872-4534-bb5d-3094216f00bd', 451809.4, '66CD94D5-9D5A-6E16-C133-96A2D86CCCDA'),
	('42125d37-950f-48e3-a08e-9acfbf085dcc', 692567.29, '18D736AB-D467-1E7B-BE01-136C86DD53DF'),
	('a57597ff-5714-4ff6-907c-34e9f12efdc6', 872200.86, '2E21EBEA-DF8A-E35B-211A-8817BF3C6156'),
	('2af22436-0a42-487f-9a6f-aa5b1c5618e3', 634252.37, '53571310-F61E-2CDA-4042-F2AA7D021A37'),
	('e71b9682-c1ae-42f2-954e-7b52cd4155b6', 809369.44, '8D7D7CD2-8681-DD58-69DE-CB842F850AEC'),
	('1b965d62-93e3-4cb9-90ac-55ce8c8b688f', 976977.81, 'DCB85C93-9CE3-B74D-AE95-F6B8197A1051'),
	('72948bb5-e07c-47eb-b8dc-05c383000567', 346520.94, '696A333D-DD17-522C-801F-67324037A32B'),
	('15c7aead-af67-42d5-bbe9-11ee244f9155', 325588.26, 'A08D6F7B-2D6A-2C77-7166-28A2A2495D82'),
	('dd1a3d62-4180-4dcc-bfb9-307a23616f56', 182899.58, 'C1DAAA63-60B7-D2B6-5065-DBA6CD2D251A'),
	('c7b74f16-9403-44b5-95ab-5937ba498cdb', 692090.23, '2590D3B8-CAC6-63A4-47C9-B14C83BC3C3E'),
	('2638cfe2-40cb-465a-97ec-1882d0ca7b1e', 953016.64, 'DFE7890A-27FC-1976-0C6A-276E1D362A21'),
	('bcf9b72f-7202-4446-a138-ba2ef5b0b291', 694463.12, 'A943C858-298A-35CA-5DDA-3BC030A52275'),
	('4d2fd41d-d93e-4197-91cd-1d98af83635c', 709459.88, 'BBE75196-A13A-F63C-5F31-768B4352A8D6'),
	('fe7b4887-0409-46de-8d9b-f9bf0350cee8', 977664.09, 'C8895712-165D-75B1-95D8-59C06D3E52CA'),
	('a6085d40-f68b-4109-a2a8-6a6c5d9eb9c3', 947549.35, '3C074137-9991-5C94-3C2A-5B9AAC2CF5E6'),
	('bca7d4ef-55eb-4bb6-9c0d-8c898f27096f', 149770.17, '2362CC29-5DFD-A69C-1AE4-9F14ED3D11C2');

INSERT INTO employees (id, name, title, payroll, department_id, project_id)
VALUES
	('c941f441-d47a-46cb-b0c9-01679ba500dd', 'Jorgan Mandeville', 'Research Associate', 86013.47, 3, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('58697f0d-86be-4ebf-9afa-ffb091338821', 'Adams Jackalin', 'Staff Scientist', 115309.32, 1, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('371a0eb3-4a05-4b3e-88e2-026e66e23271', 'Kalie Kerne', 'Director of Sales', 112434.98, 2, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('d5fdf5ea-37d7-429b-ad56-e2b05570e217', 'Fallon Fermer', 'Operator', 57551.73, 2, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('7862321a-00e6-4df5-af18-b2f2b9727196', 'Yuri Titlow', 'Compensation Analyst', 68465.26, 1, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('bf199099-1f82-423e-84c2-18f1f262b9aa', 'Ambrosi Fluin', 'Dental Hygienist', 77856.71, 2, '72cc0e39-d8f5-4751-9997-ba3e3f62639b'),
	('2e0566c9-eb0f-4109-96e2-7b3bf8bfee48', 'Damon Mickleborough', 'Teacher', 71508.16, 6, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('1912148f-9a2e-4f79-80ca-8812e0e48f12', 'Remington Popov', 'Nuclear Power Engineer', 87633.64, 5, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('884e5fca-ace2-4705-9ace-8a4aca89d08b', 'Rodger Lorek', 'Nuclear Power Engineer', 65647.25, 4, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('eafcc35d-f88c-443e-b25a-b0068cf2d44d', 'Drucill Caudrelier', 'Professor', 140232.78, 2, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('98c86345-81f7-4a05-8da8-1d6f45626715', 'Sharon Paulot', 'Product Engineer', 69873.87, 1, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('b96227d8-c2a9-4f88-9c91-7363b0d52aa7', 'Hanan Turgoose', 'Research Assistant IV', 118362.5, 4, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('6a065de2-7b5d-4d72-b1ee-6f717d2b074e', 'Susann Pomphrey', 'Technical Writer', 149125.35, 6, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('f6c2e312-7f1b-47d4-9c20-e6af65a98289', 'Marwin Santora', 'VP Sales', 74205.22, 4, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('e8504bea-5bb9-4bb1-b6ed-8678fb42c100', 'Kris Puttock', 'Tax Accountant', 145682.22, 6, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('c0727f32-7939-4490-9e8d-48f55c6398a0', 'Michal Tidridge', 'Human Resources Manager', 127858.58, 1, 'bc326b94-f32d-4f0d-bf38-15de21b3a7d4'),
	('0be8cd53-23b2-4c7f-903a-963a6ef72a22', 'Jobi Bicksteth', 'VP Accounting', 56550.32, 3, 'd530c069-20cd-4594-9bb9-a24ea37037a0'),
	('9592eb94-9c6d-41b7-8e49-03b12fbcfa74', 'Sidonnie Patis', 'Software Test Engineer IV', 127566.23, 3, 'd530c069-20cd-4594-9bb9-a24ea37037a0'),
	('b60a8f94-c1b7-41af-9f39-2f9cfabf6a28', 'Odelia Bust', 'Biostatistician III', 82878.55, 2, 'd530c069-20cd-4594-9bb9-a24ea37037a0'),
	('20858a51-92cd-4bf3-8273-46ad4c912f89', 'Christiano Gjerde', 'Product Engineer', 86369.53, 4, 'd530c069-20cd-4594-9bb9-a24ea37037a0');

INSERT INTO employees_skills
VALUES
	('c941f441-d47a-46cb-b0c9-01679ba500dd', 1),
	('c941f441-d47a-46cb-b0c9-01679ba500dd', 4),
	('c941f441-d47a-46cb-b0c9-01679ba500dd', 5),
	('58697f0d-86be-4ebf-9afa-ffb091338821', 3),
    ('58697f0d-86be-4ebf-9afa-ffb091338821', 1),
	('58697f0d-86be-4ebf-9afa-ffb091338821', 4),
	('58697f0d-86be-4ebf-9afa-ffb091338821', 5),
	('371a0eb3-4a05-4b3e-88e2-026e66e23271', 2),
    ('371a0eb3-4a05-4b3e-88e2-026e66e23271', 6),
	('371a0eb3-4a05-4b3e-88e2-026e66e23271', 7),
	('d5fdf5ea-37d7-429b-ad56-e2b05570e217', 8),
	('d5fdf5ea-37d7-429b-ad56-e2b05570e217', 9),
	('d5fdf5ea-37d7-429b-ad56-e2b05570e217', 1),
	('d5fdf5ea-37d7-429b-ad56-e2b05570e217', 6);

INSERT INTO projects_skills
VALUES
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 1),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 4),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 5),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 3),
    ('d530c069-20cd-4594-9bb9-a24ea37037a0', 1),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 4),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 5),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 2),
    ('d530c069-20cd-4594-9bb9-a24ea37037a0', 6),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 7),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 8),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 9),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 1),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 6);

INSERT INTO projects_equipments
VALUES
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'D164621A-4876-5DCC-2E33-4440A7394308'),
    ('d530c069-20cd-4594-9bb9-a24ea37037a0', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 'D164621A-4876-5DCC-2E33-4440A7394308'),
    ('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'D164621A-4876-5DCC-2E33-4440A7394308');

	
	INSERT INTO resources
	VALUES
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('72cc0e39-d8f5-4751-9997-ba3e3f62639b', 'D164621A-4876-5DCC-2E33-4440A7394308'),
    ('d530c069-20cd-4594-9bb9-a24ea37037a0', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('d530c069-20cd-4594-9bb9-a24ea37037a0', 'D164621A-4876-5DCC-2E33-4440A7394308'),
    ('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'B3D64330-EB88-B81B-CE66-A63C079EDA93'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', '388E25C7-9F49-6C18-EAEB-E20B66B5766C'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'D0E5D69F-0111-EBE6-97EE-473247B8306A'),
	('bc326b94-f32d-4f0d-bf38-15de21b3a7d4', 'D164621A-4876-5DCC-2E33-4440A7394308');
