# zmvvm - basic MVVM para csharp wpf programas 

Bindind, NotifyPropertyChanged, NotifyDataErrorInfo y DelegateCommand

## CLASES

### NotifyDataErrorInfoDictionaryClass
Clase que contiene un diccionario para mantener los errores de las validaciones de los campos  y los procedimientos relacionados con ellos. (basado en ...)

### ViewModelBindBaseClass
Clase que contiene las interfaces  InotifyPropertyChanged e INotifyDataErrorInfo 

### BasicBindClass
Clase genérica para todo tipo de datos. el tipo de data es el mismo que el del TextBox



### _BasicDoubleBindClass_
Ejemplo de binding con conversor de Data Double a textbox string

### DelegateCommandClass
La clase DelegateCommand tal y como sugiere Microsoft
### CommandClass
Clase para crear bindings a comandos mediante DelegateCommand

### ViewModelBaseClass
Clase que contiene solamente el diccionario de errores

## Ejemplo de Uso

### Clase ViewModel

~~~
using zmvvm1;
...
    public class ViewModelClass : ViewModelBaseClass
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModelClass()
        {
            codigo = new BasicBindClass<string>(notifyDataErrorInfo,validaCodigo,true);
            nombre = new BasicStringBindClass(notifyDataErrorInfo,validaNombre,true);
            importe = new BasicBindClass<double?>(notifyDataErrorInfo, validaImporte, true);
            codigov = new BasicBindClass<System.Windows.Visibility>();
           
            ejecutarComando = new CommandClass(canEjecutar, ejecutar);
            visibilidadComando = new CommandClass(null, visibilidadCodigo);
        }
        
                #region PROPIEDADES
        
        public BasicBindClass<String> codigo { get; set; }
        public BasicStringBindClass nombre { get; set; }
        public BasicBindClass <double?>importe { get; set; }
        public BasicBindClass<System.Windows.Visibility> codigov { get; set; }
        
        #endregion PROPIEDADES

        #region COMANDOS
        public CommandClass ejecutarComando { get; set; }
        public CommandClass visibilidadComando { get; set; }
        
        #endregion COMANDOS

        
        
        #region VALIDACIONES
        public  bool validaCodigo(string value, ref string sError)
        {
            if (value=="pepe")
            {
                sError = "hay error";
                return (false);
            }
            return (true);
        }
        public bool validaNombre(string value, ref string sError)
        {
           
            return (true);
        }
        public bool validaImporte(Double? value, ref string sError)
        {

            return (true);
        }

        #endregion VALIDACIONES

        #region COMANDOS
        void visibilidadCodigo(string parametro)
        {
            codigov.data = System.Windows.Visibility.Hidden;

        }
        public bool canEjecutar(string parametro)
        {
            return (true);
        }
        public void ejecutar(string parametro)
        {
            System.Windows.MessageBox.Show("al ataquer");
        }
        #endregion COMANDOS
~~~

### XALM
~~~
    <Grid>
        <TextBox HorizontalAlignment="Left" Height="23" Margin="159,50,0,0" TextWrapping="Wrap" VerticalAlignment="Top" Width="138"
         Text="{Binding codigo.b_data}"    Visibility="{Binding codigov.b_data}"    />
        <TextBox HorizontalAlignment="Left" Height="26" Margin="159,91,0,0" TextWrapping="Wrap"  VerticalAlignment="Top" Width="138"
         Text="{Binding nombre.b_data}"        />


        <Button Content="Button" HorizontalAlignment="Left" Height="26" Margin="187,197,0,0" VerticalAlignment="Top" Width="67" Command="{Binding ejecutarComando.command}"/>
        <TextBox HorizontalAlignment="Left" Height="24" Margin="159,146,0,0" TextWrapping="Wrap"  VerticalAlignment="Top" Width="138"
         Text="{Binding importe.b_data}"       />
        <Button Content="Visibilidad" HorizontalAlignment="Left" Height="26" Margin="331,197,0,0" VerticalAlignment="Top" Width="58" Command="{Binding visibilidadComando.command}"/>

    </Grid>

~~~
