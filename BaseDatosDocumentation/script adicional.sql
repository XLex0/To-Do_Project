/*==============================================================*/
/* Table: ASSIGNATION                                           */
/*==============================================================*/
create table ASSIGNATION (
   IDASSIGNATION        serial               primary key,
   IDTASK               int                  not null,
   IDLABEL              int                  not null
);

/*==============================================================*/
/* Index: PERTENECE2_FK                                         */
/*==============================================================*/
create index PERTENECE2_FK on ASSIGNATION (
   IDLABEL
);

/*==============================================================*/
/* Index: PERTENECE_FK                                          */
/*==============================================================*/
create index PERTENECE_FK on ASSIGNATION (
   IDTASK
);

/*==============================================================*/
/* Table: CATEGORYLABEL                                         */
/*==============================================================*/
create table CATEGORYLABEL (
   IDLABEL              serial               primary key,
   IDUSER               int                  null,
   NAME                 varchar(256)         not null,
   DESCRIPTION          char(256)            not null
);

/*==============================================================*/
/* Index: CREA_FK                                               */
/*==============================================================*/
create index CREA_FK on CATEGORYLABEL (
   IDUSER
);

/*==============================================================*/
/* Table: TASK                                                  */
/*==============================================================*/
create table TASK (
   IDTASK               serial               primary key,
   IDUSER               int                  null,
   DESCRIPTION          char(256)            not null,
   PRIORITY             int                  not null,
   STARTDATE            timestamp            not null,
   ENDDATE              timestamp            not null
);

/*==============================================================*/
/* Index: TIENE_FK                                              */
/*==============================================================*/
create index TIENE_FK on TASK (
   IDUSER
);

/*==============================================================*/
/* Table: "USER"                                                */
/*==============================================================*/
create table "USER" (
   IDUSER               serial               primary key,
   USERNAME             varchar(256)         not null unique,
   PASSWORD             varchar(256)         not null,
   EMAIL                char(256)            not null unique
);

/*==============================================================*/
/* Foreign Keys                                                 */
/*==============================================================*/
alter table ASSIGNATION
   add constraint FK_ASSIGNAT_PERTENECE_TASK foreign key (IDTASK)
      references TASK (IDTASK);

alter table ASSIGNATION
   add constraint FK_ASSIGNAT_PERTENECE_CATEGORY foreign key (IDLABEL)
      references CATEGORYLABEL (IDLABEL);

alter table CATEGORYLABEL
   add constraint FK_CATEGORY_CREA_USER foreign key (IDUSER)
      references "USER" (IDUSER);

alter table TASK
   add constraint FK_TASK_TIENE_USER foreign key (IDUSER)
      references "USER" (IDUSER);

-- Inserts para la tabla "USER"
insert into "USER" (USERNAME, PASSWORD, EMAIL)
values 
('franchesco', 'hola123', 'ejemplo1@epn.edu.ec'),
('alexander', 'arqui123', 'alexander.motoche@epn.edu.ec'),
('anderson01', 'admin10', 'anderson.cango@epn.edu.ec');

-- Inserts para la tabla TASK
insert into TASK (IDUSER, DESCRIPTION, PRIORITY, STARTDATE, ENDDATE)
values 
(1, 'Tarea 10 diseño', 1, '2024-12-01 09:00:00', '2024-12-10 17:00:00'),
(2, 'Entrevista Gestion', 2, '2024-12-02 10:00:00', '2024-12-02 12:00:00'),
(3, 'Comprar Regalos', 3, '2024-12-05 14:00:00', '2024-12-24 16:00:00');

-- Inserts para la tabla CATEGORYLABEL
insert into CATEGORYLABEL (IDUSER, NAME, DESCRIPTION)
values 
(1, 'Trabajo Grupal', 'Actividades relacionadas con el trabajo'),
(2, 'Personal', 'Actividades personales y recordatorios'),
(3, 'Navidad', 'Festividades cercanas');

-- Inserts para la tabla ASSIGNATION
insert into ASSIGNATION (IDTASK, IDLABEL)
values 
(1, 1),
(2, 2),
(3, 3);

SELECT * FROM "USER";
SELECT * FROM TASK;
SELECT * FROM CATEGORYLABEL;
SELECT * FROM ASSIGNATION;

-- Visualizar nombre del usuario, tarea, la prioridad y la cateogria a la que pertenece la tarea.
SELECT 
    U.USERNAME AS Usuario,
    T.DESCRIPTION AS Tarea,
    T.PRIORITY AS Prioridad,
    C.NAME AS Categoria
FROM TASK T
JOIN "USER" U ON T.IDUSER = U.IDUSER
LEFT JOIN ASSIGNATION A ON T.IDTASK = A.IDTASK
LEFT JOIN CATEGORYLABEL C ON A.IDLABEL = C.IDLABEL;

-- Inserts adicionales
select * from Task

insert into TASK (IDUSER, DESCRIPTION, PRIORITY, STARTDATE, ENDDATE)
values 
(3, 'Comprar Pastel', 2, '2024-12-07 14:00:00', '2024-12-7 16:00:00');

insert into ASSIGNATION (IDTASK, IDLABEL)
values 
(4, 3);