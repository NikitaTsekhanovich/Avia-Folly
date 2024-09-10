namespace Airplanes
{
    public class Airplane : Aircraft
    {
        public override void DoDestroy()
        {
            Destroy(gameObject);
        }
    }
}

