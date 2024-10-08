﻿using CarvedRock.Admin.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarvedRock.Admin.Data;

public class AdminContext : IdentityDbContext<AdminUser>
{
    private readonly string _dbPath;
    public AdminContext(IConfiguration config)
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _dbPath = Path.Join(path, config.GetConnectionString("UserDbFilename"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={_dbPath}");


}
