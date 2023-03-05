namespace AutoLotDAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using AutoLotDAL.Models.Base;

    public partial class Order : EntityBase
    {
        public int CustId { get; set; }

        public int CarId { get; set; }


        //[Column("CarId")]
        //public int Foo { get; set; }
        //[ForeignKey(nameof(Foo))]
        [ForeignKey(nameof(CustId))]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Inventory Inventory { get; set; }
    }
}
