//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Games.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public game()
        {
            this.comment = new HashSet<comment>();
        }
        public static string[] categories = {"Action", "Adventure","Arcade", "Dress up", "Doctor", "Kitchen","Logic","Sports"};

        public int Id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string link { get; set; }
        public Nullable<int> width { get; set; }
        public Nullable<int> height { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public Nullable<int> playcount { get; set; }
        public List<int> totalRating {
            get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comment> comment { get; set; }
    }
}