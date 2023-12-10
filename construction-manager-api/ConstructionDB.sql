use ConstructionDB;



CREATE TABLE locations (
	locations_id char(36),
    name varchar(255) NOT NULL ,
    PRIMARY KEY(locations_id)
);

CREATE TABLE departments (
	department_id int identity(1,1) primary key ,
    name varchar(50) NOT NULL, 
   /* PRIMARY KEY(department_id)*/
   location_id char(36),
    CONSTRAINT fk_projects_location_id_locations FOREIGN KEY (location_id) REFERENCES locations(locations_id) ON DELETE SET NULL ON UPDATE CASCADE
);



CREATE TABLE projects (
	project_id char(36),
    expenses decimal(10,2),
    location_id char(36),
    PRIMARY KEY(project_id),
    CONSTRAINT fk_projects_location_id_locations FOREIGN KEY (location_id) REFERENCES locations(locations_id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE skills (
/*does  the skills table need a project_id*/
	skill_id int identity(1,1) primary key,/*do we need to auto increment this? */
    name varchar(50) NOT NULL ,
   /* PRIMARY KEY (skill_id)*/
	project_id char(36),
   
    
	CONSTRAINT fk_projects_skills_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE ON UPDATE CASCADE
    /*CONSTRAINT fk_projects_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(skill_id) ON DELETE CASCADE ON UPDATE CASCADE*/ /*name needs to be FK*/
);

CREATE TABLE equipment (
    equipment_id char(36),
    name varchar(50) NOT NULL ,
    PRIMARY KEY(equipment_id),
	/*probably should be a foreign key to the locations table, since certain equipment will be 
	stored at certain locations*/
	location_id char(36),
	 CONSTRAINT fk_projects_location_id_locations FOREIGN KEY (location_id) REFERENCES locations(locations_id) ON DELETE SET NULL ON UPDATE CASCADE


);





CREATE TABLE employees (
    employee_id char(36),
    name varchar (30) NOT NULL ,
    title varchar (50),
    payroll decimal(10,2),
    department_id int,
    project_id char(36),
	location_id char(36),
    PRIMARY KEY(employee_id),
    CONSTRAINT fk_employees_department_id_departments FOREIGN KEY (department_id) REFERENCES departments(department_id) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT fk_employees_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE SET NULL ON UPDATE CASCADE,
	CONSTRAINT fk_employees_location_id_locations FOREIGN KEY (location_id) REFERENCES locations(locations_id) ON DELETE SET NULL ON UPDATE CASCADE
);


CREATE TABLE employees_skills (
	employee_id char(36),
    skill_id int,
	
    PRIMARY KEY (employee_id, skill_id),
    CONSTRAINT fk_employees_skills_employee_id_employees FOREIGN KEY (employee_id) REFERENCES employees(employee_id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_employees_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(skill_id) ON DELETE CASCADE ON UPDATE CASCADE
	);
);

CREATE TABLE projects_skills (
	project_id char(36),
    skill_id int,
    PRIMARY KEY (project_id, skill_id),
    CONSTRAINT fk_projects_skills_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_projects_skills_skill_id_skills FOREIGN KEY (skill_id) REFERENCES skills(skill_id) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE projects_equipments (
	project_id char(36),
    equipment_id char(36),
    PRIMARY KEY (project_id, equipment_id),
    CONSTRAINT fk_projects_equipments_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT fk_projects_equipments_equipment_id_equipments FOREIGN KEY (equipment_id) REFERENCES equipments(equipment_id) ON DELETE CASCADE ON UPDATE CASCADE
);



CREATE TABLE resources(

 resource_id char(36) primary key,
 project_id INT,
 location_id int,
resource_type char(20),
 CONSTRAINT fk_reources_project_id_projects FOREIGN KEY (project_id) REFERENCES projects(project_id) ON DELETE CASCADE ON UPDATE CASCADE,
 CONSTRAINT fk_resources_locations_id_projects FOREIGN KEY (location_id) REFERENCES locations(locations_id) ON DELETE CASCADE ON UPDATE CASCADE

);


 


