 create table Risk  
(  
RiskId int identity(1,1) primary key,  RiskName varchar(100)
); 

create table gridview  
(  
sid int identity(1,1) primary key,  
name varchar(100),  
address varchar(200),
RiskId int foreign key references Risk(RiskId)
);  
 
insert into gridview (name,address,RiskId) values('Mukesh Kumar','New Delhi',1)  
insert into gridview (name,address,RiskId) values('Deepa kumar','Utra Khand',2)  
insert into gridview (name,address,RiskId) values('Pankaj Kumar','New Delhi',3)  
insert into gridview (name,address,RiskId) values('Sanjeev Kumar','New Delhi',1)  
insert into gridview (name,address,RiskId) values('Pawan Kumar','New Delhi',2) 

insert into Risk (RiskName) values('Risk 1')  
insert into Risk (RiskName) values('Risk 2')  
insert into Risk (RiskName) values('Risk 3')  
  