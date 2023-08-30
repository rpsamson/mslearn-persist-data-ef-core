using System;
using System.Collections.Generic;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data;

public partial class PromotionsContext : DbContext
{
 
    public PromotionsContext(DbContextOptions<PromotionsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coupon> Coupons => Set<Coupon>();

}
