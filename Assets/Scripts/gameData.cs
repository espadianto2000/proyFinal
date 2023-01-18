using System;

[Serializable]
public class gameData
{
    public int dinero = 0;
    public int nivelVidaExtra = 0;
    public int nivelVelocidadExtra = 0;
    public int nivelDanoExtra = 0;
    public int nivelCritExtra = 0;
    public int nivelExpExtra = 0;
    public int nivelPuntosExtra = 0;
    public int nivelDineroExtra = 0;
    public int nivelSpawnVida = 0;
    public int nivelCuracionExtra = 0;
    public int nivelVelocidadAtaqueExtra = 0;
    public bool desbloquearPersonaje2 = false;
    public bool desbloquearUlti = false;
    public float highScore = 0;
    public gameData(int din, int vida, int vel, int dano, int crit, int exp, int pts, int dinex, int spvida, int cur, int velat, bool pj2, bool ulti, float hs)
    {
        this.dinero = din;
        this.nivelVidaExtra = vida;
        this.nivelVelocidadExtra = vel;
        this.nivelDanoExtra = dano;
        this.nivelCritExtra = crit;
        this.nivelExpExtra = exp;
        this.nivelPuntosExtra = pts;
        this.nivelDineroExtra = dinex;
        this.nivelSpawnVida = spvida;
        this.nivelCuracionExtra = cur;
        this.nivelVelocidadAtaqueExtra = velat;
        this.desbloquearPersonaje2 = pj2;
        this.desbloquearUlti = ulti;
        this.highScore = hs;
    }
}
