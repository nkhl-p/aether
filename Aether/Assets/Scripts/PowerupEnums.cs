public enum PowerupEnums {
    TIME,
    PERMEATE,
    LEVITATE,
    SIZE,
	SPEED,
    SHOOT,
    NONE
}

public static class PowerupExtensions {
    public static string GetString(this PowerupEnums me) {
        switch (me) {
            case PowerupEnums.TIME:
                return "Time";
            case PowerupEnums.PERMEATE:
                return "Permeate";
            case PowerupEnums.LEVITATE:
                return "Levitate";
            case PowerupEnums.SIZE:
                return "Size";
			case PowerupEnums.SPEED:
                return "Speed";
            case PowerupEnums.SHOOT:
                return "Shoot";
            case PowerupEnums.NONE:
                return "";
            default:
                return "";
        }
    }
}
