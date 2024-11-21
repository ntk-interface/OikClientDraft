namespace OikClientDraft;

// TODO внести изменения для своего приложения
public static class Vars
{
  public const string TraceName = "OikClientDraft"; // название приложения для трассировки сервера ОИК

  // свойства главного окна
  public const string WindowTitle  = "ОИК Диспетчер НТ"; // название приложения для заголовка окна и панели задач
  public const int    WindowWidth  = 1000;               // ширина окна
  public const int    WindowHeight = 600;                // высота окна

  // реквизиты сервера ОИК по умолчанию, можно не изменять, а задавать аргументами при запуске программы
  public const string DefaultOikHost     = "127.0.0.1"; // адрес сервера ОИК
  public const string DefaultOikUser     = "";          // имя пользователя
  public const string DefaultOikPassword = "";          // пароль

  // разное
  public const int DefaultUpdateInterval = 2; // интервал для обновления данных на странице
}
