//using system.collections;
//using system.collections.generic;
//using unityengine;
//using unityengine.inputsystem;

//public class player : aentity, inputcontrol.iplayeractions
//{
//    const string axisx = "x", axisy = "y", paramismoving = "ismoving";

//    private animator _animator;
//    private vector2 _mouseposition;

//    private void awake()
//    {
//        _animator = getcomponent<animator>();
//    }
//    private void update()
//    {
//        _mouseposition = getmouseposition() - (vector2) transform.position; //posicion del raton respecto al jugador.
//        animationbydirection();
//    }

//    public void onmovement(inputaction.callbackcontext context)
//    {
//        if (context.performed)
//        {
//            //leemos la configuracion de las teclas de movimiento. ex: 'w' = (0,1)
//            _inputmovement = context.readvalue<vector2>();
//            debug.log(_inputmovement);

//            _animator.setbool(paramismoving, true);
//        }
//        else if (context.canceled)
//        {
//            _inputmovement = vector2.zero;
//            _animator.setbool(paramismoving, false);
//        }
//    }

//    //obtenemos la posicion del raton.
//    private vector2 getmouseposition()
//    {
//        return camera.main.screentoworldpoint(mouse.current.position.readvalue());
//    }

//    //la animacion de movimiento dependera del input del teclado
//    private void animationbydirection()
//    {
//        _animator.setfloat(axisx, _mouseposition.x);
//        _animator.setfloat(axisy, _mouseposition.y);
//    }
//}
