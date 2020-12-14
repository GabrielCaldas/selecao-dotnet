using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using API_de_cursos.Models;

namespace API_de_cursos
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddDbContext<AlunoContext>(opt => opt.UseInMemoryDatabase("AlunoList"));
			services.AddDbContext<AlunoCartaoContext>(opt => opt.UseInMemoryDatabase("AlunoCartaoList"));
			services.AddDbContext<AlunoPGTContext>(opt => opt.UseInMemoryDatabase("AlunoPagamentoList"));
			services.AddDbContext<CursoContext>(opt => opt.UseInMemoryDatabase("CursoList"));
			services.AddDbContext<MatriculaContext>(opt => opt.UseInMemoryDatabase("MatriculaList"));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_de_cursos", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_de_cursos v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
