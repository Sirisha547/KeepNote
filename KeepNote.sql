create database KeepNoteDB

use KeepNoteDB

create table note
(
id int identity primary key,
title varchar(50),
descrp varchar(80),
dates date
)



select * from note
 