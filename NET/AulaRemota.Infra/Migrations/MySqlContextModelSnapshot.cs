﻿// <auto-generated />
using System;
using AulaRemota.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AulaRemota.Infra.Migrations
{
    [DbContext(typeof(MySqlContext))]
    partial class MySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ApiUserModelRolesModel", b =>
                {
                    b.Property<int>("ApiUsersId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("ApiUsersId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ApiUserModelRolesModel");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.AddressModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(12)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("District")
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Uf")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.Auth.ApiUserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("ApiUser");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<int>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Identity")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Origin")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Administrative");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.CourseModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Workload")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("CorporateName")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("FantasyName")
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("FoundingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Site")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("StateRegistration")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DrivingSchool");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Identity")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Origin")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<int>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Identity")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Origin")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ClassId");

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.TurmaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingLevelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.HasKey("Id");

                    b.ToTable("EdrivingLevel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Level = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = 2,
                            Level = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Level = "DIRETOR"
                        });
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LevelId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Edriving");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Destiny")
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<string>("Extension")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FileName")
                        .HasColumnType("varchar(150)");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("InstructorId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PartnnerLevelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.HasKey("Id");

                    b.ToTable("PartnnerLevel");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Level = "ADMINISTRATIVO"
                        },
                        new
                        {
                            Id = 2,
                            Level = "ANALISTA"
                        },
                        new
                        {
                            Id = 3,
                            Level = "EMPRESA"
                        });
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PartnnerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("LevelId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Partnner");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PhoneModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AdministrativeId")
                        .HasColumnType("int");

                    b.Property<int?>("DrivingSchoolId")
                        .HasColumnType("int");

                    b.Property<int?>("EdrivingId")
                        .HasColumnType("int");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<int?>("PartnnerId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdministrativeId");

                    b.HasIndex("DrivingSchoolId");

                    b.HasIndex("EdrivingId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("PartnnerId");

                    b.HasIndex("StudentId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.RolesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(70)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CourseModelTurmaModel", b =>
                {
                    b.Property<int>("ClassesId")
                        .HasColumnType("int");

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.HasKey("ClassesId", "CoursesId");

                    b.HasIndex("CoursesId");

                    b.ToTable("CourseModelTurmaModel");
                });

            modelBuilder.Entity("DrivingSchoolModelInstructorModel", b =>
                {
                    b.Property<int>("DrivingScoolsId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorsId")
                        .HasColumnType("int");

                    b.HasKey("DrivingScoolsId", "InstructorsId");

                    b.HasIndex("InstructorsId");

                    b.ToTable("DrivingSchoolModelInstructorModel");
                });

            modelBuilder.Entity("RolesModelUserModel", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RolesModelUserModel");
                });

            modelBuilder.Entity("ApiUserModelRolesModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.Auth.ApiUserModel", null)
                        .WithMany()
                        .HasForeignKey("ApiUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.RolesModel", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.AddressModel", "Address")
                        .WithOne("Administrative")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("Administratives")
                        .HasForeignKey("DrivingSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("Administrative")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("DrivingSchool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.CourseModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("Courses")
                        .HasForeignKey("DrivingSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "Instructor")
                        .WithMany("Courses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrivingSchool");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.AddressModel", "Address")
                        .WithOne("DrivingSchool")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("DrivingSchool")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.AddressModel", "Address")
                        .WithOne("Instructor")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("Instructor")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.StudentModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.AddressModel", "Address")
                        .WithMany("Students")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.TurmaModel", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("Sudents")
                        .HasForeignKey("DrivingSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("Student")
                        .HasForeignKey("AulaRemota.Infra.Entity.DrivingSchool.StudentModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Class");

                    b.Navigation("DrivingSchool");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.TurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("Classes")
                        .HasForeignKey("DrivingSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrivingSchool");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.EdrivingLevelModel", "Level")
                        .WithMany("Edrivings")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("Edriving")
                        .HasForeignKey("AulaRemota.Infra.Entity.EdrivingModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.FileModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("Files")
                        .HasForeignKey("DrivingSchoolId");

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "Instructor")
                        .WithMany("Files")
                        .HasForeignKey("InstructorId");

                    b.Navigation("DrivingSchool");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PartnnerModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.AddressModel", "Address")
                        .WithOne("Partnner")
                        .HasForeignKey("AulaRemota.Infra.Entity.PartnnerModel", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.PartnnerLevelModel", "Level")
                        .WithMany("Partnners")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", "User")
                        .WithOne("Partnner")
                        .HasForeignKey("AulaRemota.Infra.Entity.PartnnerModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Level");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PhoneModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", "Administrative")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("AdministrativeId");

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", "DrivingSchool")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("DrivingSchoolId");

                    b.HasOne("AulaRemota.Infra.Entity.EdrivingModel", "Edriving")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("EdrivingId");

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", "Instructor")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("InstructorId");

                    b.HasOne("AulaRemota.Infra.Entity.PartnnerModel", "Partnner")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("PartnnerId");

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.StudentModel", "Student")
                        .WithMany("PhonesNumbers")
                        .HasForeignKey("StudentId");

                    b.Navigation("Administrative");

                    b.Navigation("DrivingSchool");

                    b.Navigation("Edriving");

                    b.Navigation("Instructor");

                    b.Navigation("Partnner");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CourseModelTurmaModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.TurmaModel", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.CourseModel", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrivingSchoolModelInstructorModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", null)
                        .WithMany()
                        .HasForeignKey("DrivingScoolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", null)
                        .WithMany()
                        .HasForeignKey("InstructorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolesModelUserModel", b =>
                {
                    b.HasOne("AulaRemota.Infra.Entity.RolesModel", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AulaRemota.Infra.Entity.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.AddressModel", b =>
                {
                    b.Navigation("Administrative");

                    b.Navigation("DrivingSchool");

                    b.Navigation("Instructor");

                    b.Navigation("Partnner");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.AdministrativeModel", b =>
                {
                    b.Navigation("PhonesNumbers");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.DrivingSchoolModel", b =>
                {
                    b.Navigation("Administratives");

                    b.Navigation("Classes");

                    b.Navigation("Courses");

                    b.Navigation("Files");

                    b.Navigation("PhonesNumbers");

                    b.Navigation("Sudents");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.InstructorModel", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Files");

                    b.Navigation("PhonesNumbers");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.StudentModel", b =>
                {
                    b.Navigation("PhonesNumbers");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.DrivingSchool.TurmaModel", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingLevelModel", b =>
                {
                    b.Navigation("Edrivings");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.EdrivingModel", b =>
                {
                    b.Navigation("PhonesNumbers");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PartnnerLevelModel", b =>
                {
                    b.Navigation("Partnners");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.PartnnerModel", b =>
                {
                    b.Navigation("PhonesNumbers");
                });

            modelBuilder.Entity("AulaRemota.Infra.Entity.UserModel", b =>
                {
                    b.Navigation("Administrative");

                    b.Navigation("DrivingSchool");

                    b.Navigation("Edriving");

                    b.Navigation("Instructor");

                    b.Navigation("Partnner");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
