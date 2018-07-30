
namespace GasMileageWPF_MVVM.Model
{
    class Automobile
    {
        public int vin { get; set; }
        public string auto { get; set; }

        public Automobile(string automobile)
        {
            auto = automobile;
        }
    }
}
