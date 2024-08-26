using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HarmonyHotles.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }



    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aboutu> Aboutus { get; set; }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Contactinfo> Contactinfos { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Footerlink> Footerlinks { get; set; }

    public virtual DbSet<Footersetting> Footersettings { get; set; }

    public virtual DbSet<Header> Headers { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Hotelamenity> Hotelamenities { get; set; }

    public virtual DbSet<Hotelservice> Hotelservices { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Roomamenity> Roomamenities { get; set; }

    public virtual DbSet<Roomtype> Roomtypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slider> Sliders { get; set; }

    public virtual DbSet<Socialmedialink> Socialmedialinks { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##admin;PASSWORD=Test321;DATA SOURCE=localhost:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##ADMIN")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Aboutu>(entity =>
        {
            entity.HasKey(e => e.Aboutusid).HasName("SYS_C009921");

            entity.ToTable("ABOUTUS");

            entity.Property(e => e.Aboutusid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ABOUTUSID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Imagepath1)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH1");
            entity.Property(e => e.Imagepath2)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH2");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.Amenityid).HasName("SYS_C009865");

            entity.ToTable("AMENITY");

            entity.Property(e => e.Amenityid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AMENITYID");
            entity.Property(e => e.Amenityname)
                .HasMaxLength(100)
                .HasColumnName("AMENITYNAME");
            entity.Property(e => e.Amenitytype)
                .HasMaxLength(100)
                .HasColumnName("AMENITYTYPE");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("SYS_C009883");

            entity.ToTable("BANK");

            entity.Property(e => e.Accountid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCOUNTID");
            entity.Property(e => e.Balance)
                .HasColumnType("NUMBER(18,2)")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cardnumber)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CARDNUMBER");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Cvv)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CVV");
            entity.Property(e => e.Expirationdate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRATIONDATE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Banks)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009884");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("SYS_C009886");

            entity.ToTable("BOOKINGS");

            entity.Property(e => e.Bookingid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGID");
            entity.Property(e => e.Bookingstatus)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Confirmed'")
                .HasColumnName("BOOKINGSTATUS");
            entity.Property(e => e.Checkindate)
                .HasColumnType("DATE")
                .HasColumnName("CHECKINDATE");
            entity.Property(e => e.Checkoutdate)
                .HasColumnType("DATE")
                .HasColumnName("CHECKOUTDATE");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("SYSDATE")
                .HasColumnType("DATE")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Eventid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EVENTID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Servicesid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESID");
            entity.Property(e => e.Totalprice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TOTALPRICE");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("UPDATEDAT");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009891");

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("SYS_C009889");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009887");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("SYS_C009888");

            entity.HasOne(d => d.Services).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Servicesid)
                .HasConstraintName("SYS_C009890");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Cityid).HasName("SYS_C009830");

            entity.ToTable("CITIES");

            entity.Property(e => e.Cityid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CITYID");
            entity.Property(e => e.Cityname)
                .HasMaxLength(100)
                .HasColumnName("CITYNAME");
            entity.Property(e => e.Countryid)
                .HasColumnType("NUMBER")
                .HasColumnName("COUNTRYID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("SYS_C009831");
        });

        modelBuilder.Entity<Contactinfo>(entity =>
        {
            entity.HasKey(e => e.Contactinfoid).HasName("SYS_C009918");

            entity.ToTable("CONTACTINFO");

            entity.Property(e => e.Contactinfoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CONTACTINFOID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("CITY");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .HasColumnName("STATE");
            entity.Property(e => e.Workinghours)
                .HasMaxLength(255)
                .HasColumnName("WORKINGHOURS");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(20)
                .HasColumnName("ZIPCODE");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Countryid).HasName("SYS_C009827");

            entity.ToTable("COUNTRIES");

            entity.Property(e => e.Countryid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COUNTRYID");
            entity.Property(e => e.Countryname)
                .HasMaxLength(100)
                .HasColumnName("COUNTRYNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("SYS_C009816");

            entity.ToTable("CUSTOMERS");

            entity.HasIndex(e => e.Email, "SYS_C009817").IsUnique();

            entity.Property(e => e.Customerid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Bookingcount)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGCOUNT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("FNAME");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(100)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Isactive)
                .HasPrecision(1)
                .HasDefaultValueSql("1\n")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("LNAME");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("PHONENUMBER");
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.Destinationid).HasName("SYS_C009927");

            entity.ToTable("DESTINATIONS");

            entity.Property(e => e.Destinationid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DESTINATIONID");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Placescount)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PLACESCOUNT");
            entity.Property(e => e.Ullink)
                .HasMaxLength(255)
                .HasColumnName("ULLINK");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Eventid).HasName("SYS_C009841");

            entity.ToTable("EVENTS");

            entity.Property(e => e.Eventid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EVENTID");
            entity.Property(e => e.Cityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CITYID");
            entity.Property(e => e.Countryid)
                .HasColumnType("NUMBER")
                .HasColumnName("COUNTRYID");
            entity.Property(e => e.Eventsdescription)
                .HasColumnType("CLOB")
                .HasColumnName("EVENTSDESCRIPTION");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.Ticketprice)
                .HasMaxLength(255)
                .HasColumnName("TICKETPRICE");
            entity.Property(e => e.Timefrom)
                .HasColumnType("DATE")
                .HasColumnName("TIMEFROM");
            entity.Property(e => e.Timeto)
                .HasColumnType("DATE")
                .HasColumnName("TIMETO");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("STATUS");

            entity.HasOne(d => d.City).WithMany(p => p.Events)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("SYS_C009844");

            entity.HasOne(d => d.Country).WithMany(p => p.Events)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("SYS_C009842");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Events)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009843");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Favoriteid).HasName("SYS_C009875");

            entity.ToTable("FAVORITES");

            entity.Property(e => e.Favoriteid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FAVORITEID");
            entity.Property(e => e.Addeddate)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("ADDEDDATE");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Eventid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EVENTID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009876");

            entity.HasOne(d => d.Event).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("SYS_C009878");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009877");
        });

        modelBuilder.Entity<Footerlink>(entity =>
        {
            entity.HasKey(e => e.Footerlinkid).HasName("SYS_C009935");

            entity.ToTable("FOOTERLINKS");

            entity.Property(e => e.Footerlinkid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FOOTERLINKID");
            entity.Property(e => e.Displayorder)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DISPLAYORDER");
            entity.Property(e => e.Isactive)
                .HasPrecision(1)
                .HasDefaultValueSql("1\n")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Linktext)
                .HasMaxLength(100)
                .HasColumnName("LINKTEXT");
            entity.Property(e => e.Linkurl)
                .HasMaxLength(255)
                .HasColumnName("LINKURL");
            entity.Property(e => e.Sectionname)
                .HasMaxLength(100)
                .HasColumnName("SECTIONNAME");
        });

        modelBuilder.Entity<Footersetting>(entity =>
        {
            entity.HasKey(e => e.Footerid).HasName("SYS_C009929");

            entity.ToTable("FOOTERSETTINGS");

            entity.Property(e => e.Footerid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FOOTERID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Buttonlink)
                .HasMaxLength(255)
                .HasColumnName("BUTTONLINK");
            entity.Property(e => e.Copyrighttext)
                .HasMaxLength(255)
                .HasColumnName("COPYRIGHTTEXT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Logopath)
                .HasMaxLength(255)
                .HasColumnName("LOGOPATH");
            entity.Property(e => e.Newslettertext)
                .HasMaxLength(255)
                .HasColumnName("NEWSLETTERTEXT");
            entity.Property(e => e.Newslettertitle)
                .HasMaxLength(255)
                .HasColumnName("NEWSLETTERTITLE");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("PHONENUMBER");
        });

        modelBuilder.Entity<Header>(entity =>
        {
            entity.HasKey(e => e.Headerid).HasName("SYS_C009923");

            entity.ToTable("HEADER");

            entity.Property(e => e.Headerid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HEADERID");
            entity.Property(e => e.Isactive)
                .HasPrecision(1)
                .HasDefaultValueSql("1\n")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .HasColumnName("LOGO");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Socialmedialinks)
                .HasMaxLength(255)
                .HasColumnName("SOCIALMEDIALINKS");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Hotelid).HasName("SYS_C009835");

            entity.ToTable("HOTELS");

            entity.Property(e => e.Hotelid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Cityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CITYID");
            entity.Property(e => e.Countryid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("COUNTRYID");
            entity.Property(e => e.Hotelsdescription)
                .HasColumnType("CLOB")
                .HasColumnName("HOTELSDESCRIPTION");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("NAME");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RATING");

            entity.HasOne(d => d.City).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.Cityid)
                .HasConstraintName("SYS_C009837");

            entity.HasOne(d => d.Country).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.Countryid)
                .HasConstraintName("SYS_C009836");
        });

        modelBuilder.Entity<Hotelamenity>(entity =>
        {
            entity.HasKey(e => e.Hotelamenityid).HasName("SYS_C009867");

            entity.ToTable("HOTELAMENITY");

            entity.Property(e => e.Hotelamenityid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELAMENITYID");
            entity.Property(e => e.Amenityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AMENITYID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");

            entity.HasOne(d => d.Amenity).WithMany(p => p.Hotelamenities)
                .HasForeignKey(d => d.Amenityid)
                .HasConstraintName("SYS_C009868");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Hotelamenities)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009869");
        });

        modelBuilder.Entity<Hotelservice>(entity =>
        {
            entity.HasKey(e => e.Hotelserviceid).HasName("SYS_C009848");

            entity.ToTable("HOTELSERVICE");

            entity.Property(e => e.Hotelserviceid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELSERVICEID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Servicesid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESID");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Hotelservices)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009849");

            entity.HasOne(d => d.Services).WithMany(p => p.Hotelservices)
                .HasForeignKey(d => d.Servicesid)
                .HasConstraintName("SYS_C009850");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Imageid).HasName("SYS_C009859");

            entity.ToTable("IMAGES");

            entity.Property(e => e.Imageid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("IMAGEID");
            entity.Property(e => e.Eventid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EVENTID");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Roomtypeid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMTYPEID");

            entity.HasOne(d => d.Event).WithMany(p => p.Images)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("SYS_C009863");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Images)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009860");

            entity.HasOne(d => d.Room).WithMany(p => p.Images)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("SYS_C009861");

            entity.HasOne(d => d.Roomtype).WithMany(p => p.Images)
                .HasForeignKey(d => d.Roomtypeid)
                .HasConstraintName("SYS_C009862");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("SYS_C009904");

            entity.ToTable("MESSAGES");

            entity.Property(e => e.Messageid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MESSAGEID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message1)
                .HasMaxLength(400)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("NAME");
            entity.Property(e => e.Sentat)
                .HasColumnType("DATE")
                .HasColumnName("SENTAT");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("SUBJECT");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Notificationid).HasName("SYS_C009910");

            entity.ToTable("NOTIFICATIONS");

            entity.Property(e => e.Notificationid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("NOTIFICATIONID");
            entity.Property(e => e.Bookingid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGID");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Isread)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISREAD");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Messageid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MESSAGEID");
            entity.Property(e => e.Testimonialid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Bookingid)
                .HasConstraintName("SYS_C009913");

            entity.HasOne(d => d.Customer).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009914");

            entity.HasOne(d => d.MessageNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Messageid)
                .HasConstraintName("SYS_C009912");

            entity.HasOne(d => d.Testimonial).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Testimonialid)
                .HasConstraintName("SYS_C009911");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("SYS_C009895");

            entity.ToTable("PAYMENTS");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PAYMENTID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Bookingid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("BOOKINGID");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Eventid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("EVENTID");
            entity.Property(e => e.Paymentdate)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("PAYMENTDATE");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("PAYMENTMETHOD");
            entity.Property(e => e.Servicesid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Bookingid)
                .HasConstraintName("SYS_C009897");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009896");

            entity.HasOne(d => d.Event).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("SYS_C009898");

            entity.HasOne(d => d.Services).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Servicesid)
                .HasConstraintName("SYS_C009899");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("SYS_C009813");

            entity.ToTable("ROLES");

            entity.Property(e => e.Roleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Roomid).HasName("SYS_C009855");

            entity.ToTable("ROOMS");

            entity.Property(e => e.Roomid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");
            entity.Property(e => e.Bedtype)
                .HasMaxLength(100)
                .HasColumnName("BEDTYPE");
            entity.Property(e => e.Hotelid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTELID");
            entity.Property(e => e.Isavailable)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("ISAVAILABLE");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Roomnumber)
                .HasMaxLength(50)
                .HasColumnName("ROOMNUMBER");
            entity.Property(e => e.Roomtypeid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMTYPEID");
            entity.Property(e => e.Status)
               .HasMaxLength(20)
               .HasColumnName("STATUS");
            entity.Property(e => e.AVAILABLEFROM)
      .HasColumnType("DATE")
      .HasColumnName("AVAILABLEFROM");

            entity.Property(e => e.AVAILABLETO)
                .HasColumnType("DATE")
                .HasColumnName("AVAILABLETO");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.Hotelid)
                .HasConstraintName("SYS_C009856");

            entity.HasOne(d => d.Roomtype).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.Roomtypeid)
                .HasConstraintName("SYS_C009857");
        });

        modelBuilder.Entity<Roomamenity>(entity =>
        {
            entity.HasKey(e => e.Roomamenityid).HasName("SYS_C009871");

            entity.ToTable("ROOMAMENITY");

            entity.Property(e => e.Roomamenityid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMAMENITYID");
            entity.Property(e => e.Amenityid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AMENITYID");
            entity.Property(e => e.Roomid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMID");

            entity.HasOne(d => d.Amenity).WithMany(p => p.Roomamenities)
                .HasForeignKey(d => d.Amenityid)
                .HasConstraintName("SYS_C009873");

            entity.HasOne(d => d.Room).WithMany(p => p.Roomamenities)
                .HasForeignKey(d => d.Roomid)
                .HasConstraintName("SYS_C009872");
        });

        modelBuilder.Entity<Roomtype>(entity =>
        {
            entity.HasKey(e => e.Roomtypeid).HasName("SYS_C009853");

            entity.ToTable("ROOMTYPES");

            entity.Property(e => e.Roomtypeid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOMTYPEID");
            entity.Property(e => e.Roomsize)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("ROOMSIZE");
            entity.Property(e => e.Roomtypesdescription)
                .HasColumnType("CLOB")
                .HasColumnName("ROOMTYPESDESCRIPTION");
            entity.Property(e => e.Typename)
                .HasMaxLength(100)
                .HasColumnName("TYPENAME");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Servicesid).HasName("SYS_C009846");

            entity.ToTable("SERVICES");

            entity.Property(e => e.Servicesid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SERVICESID");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(200)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Servicename)
                .HasMaxLength(255)
                .HasColumnName("SERVICENAME");
            entity.Property(e => e.Serviceprice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("SERVICEPRICE");
            entity.Property(e => e.Status)
               .HasMaxLength(20)
               .HasColumnName("STATUS");
                    });

        modelBuilder.Entity<Slider>(entity =>
        {
            entity.HasKey(e => e.Sliderid).HasName("SYS_C009925");

            entity.ToTable("SLIDERS");

            entity.Property(e => e.Sliderid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SLIDERID");
            entity.Property(e => e.Buttonlink)
                .HasMaxLength(255)
                .HasColumnName("BUTTONLINK");
            entity.Property(e => e.Buttontext)
                .HasMaxLength(100)
                .HasColumnName("BUTTONTEXT");
            entity.Property(e => e.Description)
                .HasMaxLength(400)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Displayorder)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DISPLAYORDER");
            entity.Property(e => e.Imagepath)
                .HasMaxLength(255)
                .HasColumnName("IMAGEPATH");
            entity.Property(e => e.Isactive)
                .HasPrecision(1)
                .HasDefaultValueSql("1")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Socialmedialink>(entity =>
        {
            entity.HasKey(e => e.Socialmediaid).HasName("SYS_C009931");

            entity.ToTable("SOCIALMEDIALINKS");

            entity.Property(e => e.Socialmediaid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SOCIALMEDIAID");
            entity.Property(e => e.Footerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("FOOTERID");
            entity.Property(e => e.Headerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HEADERID");
            entity.Property(e => e.Iconclass)
                .HasMaxLength(50)
                .HasColumnName("ICONCLASS");
            entity.Property(e => e.Platformname)
                .HasMaxLength(100)
                .HasColumnName("PLATFORMNAME");
            entity.Property(e => e.Urllink)
                .HasMaxLength(255)
                .HasColumnName("URLLINK");

            entity.HasOne(d => d.Footer).WithMany(p => p.Socialmedialinks)
                .HasForeignKey(d => d.Footerid)
                .HasConstraintName("SYS_C009933");

            entity.HasOne(d => d.Header).WithMany(p => p.SocialmedialinksNavigation)
                .HasForeignKey(d => d.Headerid)
                .HasConstraintName("SYS_C009932");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Testimonialid).HasName("SYS_C009907");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.Testimonialid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIALID");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("CREATEDAT");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Isapproved)
                .HasPrecision(1)
                .HasDefaultValueSql("0")
                .HasColumnName("ISAPPROVED");
            entity.Property(e => e.Message)
                .HasColumnType("CLOB")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RATING");

            entity.HasOne(d => d.Customer).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009908");
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.UlId).HasName("SYS_C009821");

            entity.ToTable("USER_LOGIN");

            entity.HasIndex(e => e.Username, "SYS_C009822").IsUnique();

            entity.Property(e => e.UlId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("UL_ID");
            entity.Property(e => e.Customerid)
                .HasColumnType("NUMBER")
                .HasColumnName("CUSTOMERID");
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Roleid)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLEID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("SYS_C009824");

            entity.HasOne(d => d.Role).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("SYS_C009823");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Videoid).HasName("SYS_C009937");

            entity.ToTable("VIDEO");

            entity.Property(e => e.Videoid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VIDEOID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("TITLE");
            entity.Property(e => e.Videopath)
                .HasMaxLength(255)
                .HasColumnName("VIDEOPATH");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
