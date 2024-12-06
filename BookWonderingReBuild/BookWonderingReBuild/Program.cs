using BookWonderingReBuild.Service.Config;
using BookWonderingReBuild.Service.User;
using BookWonderingReBuild.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserService,UserService>();

//注册JWT
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
builder.Services.AddTransient<ICustomJWTService, CustomJWTService>();


#region jwt校验  HS
{
    //第二步，增加鉴权逻辑
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT有一些默认的属性，就是给鉴权时就可以筛选了
             ValidateIssuer = true,//是否验证Issuer
             ValidateAudience = true,//是否验证Audience
             ValidateLifetime = true,//是否验证失效时间
             ValidateIssuerSigningKey = true,//是否验证SecurityKey
             ValidAudience = tokenOptions.Audience,//
             ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey 
         };
     });
}
#endregion
//添加跨域策略
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("X-Pagination"));
});



//日志文件
builder.Logging.AddLog4Net("CfgFile/log4net.Config");

//Automapper映射
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
