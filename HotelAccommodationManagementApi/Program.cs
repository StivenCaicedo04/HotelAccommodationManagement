using HotelAccommodationManagementApplication.Services;
using HotelAccommodationManagementDomain.Repositories;
using HotelAccommodationManagementDomain.Repositories.Repository;
using HotelAccommodationManagementInfrastructure.Data;
using HotelAccommodationManagementInfrastructure.Mapper;
using HotelAccommodationManagementInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura el DbContext
builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISendMail, MailServices>();
builder.Services.AddScoped<ILoginUserRepository, LoginRepository>();

builder.Services.AddScoped<HotelServices>();
builder.Services.AddScoped<PassengerService>();
builder.Services.AddScoped<RoomServices>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReservationServices>();
builder.Services.AddScoped<LoginServices>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
