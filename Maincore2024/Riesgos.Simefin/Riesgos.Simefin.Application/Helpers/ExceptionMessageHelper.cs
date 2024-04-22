namespace Riesgos.Simefin.Application.Helpers
{

    /// <summary>
    /// Mensajes de excepciones
    /// </summary>
    public class ExceptionMessageHelper
    {

        /// <summary>
        /// Recuperar mensajes de error anidados
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(Exception exception)
        {
            string message = exception.Message + " ";
            while (exception?.InnerException != null)
            {
                message += exception.InnerException?.Message;
                exception = exception.InnerException!;
            }

            return message;
        }

    }

}
