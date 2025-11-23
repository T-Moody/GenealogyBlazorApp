using GenealogyBlazorApp.Client.Features.Home.Models;
using Microsoft.AspNetCore.Components;

namespace GenealogyBlazorApp.Client.Features.Home.Components;

public partial class CountyCards
{
    [Parameter] 
    public HomeState State { get; set; } = default!;

    [Parameter] 
    public bool IsEditor { get; set; }

    private class CountyModel
    {
        public string Id { get; set; } = "";
        public Func<string> GetTitle { get; set; } = () => "";
        public Action<string> SetTitle { get; set; } = _ => { };
        public string Link { get; set; } = "";
        public string ButtonText { get; set; } = "Explore Records";
    }

    private IEnumerable<CountyModel> Counties => new List<CountyModel>
    {
        new() 
        { 
            Id = "huron", 
            GetTitle = () => State.HuronTitle, 
            SetTitle = v => State.HuronTitle = v,
            Link = "/counties/huron"
        },
        new() 
        { 
            Id = "sanilac", 
            GetTitle = () => State.SanilacTitle, 
            SetTitle = v => State.SanilacTitle = v,
            Link = "/counties/sanilac"
        },
        new() 
        { 
            Id = "tuscola", 
            GetTitle = () => State.TuscolaTitle, 
            SetTitle = v => State.TuscolaTitle = v,
            Link = "/counties/tuscola"
        }
    };
}
