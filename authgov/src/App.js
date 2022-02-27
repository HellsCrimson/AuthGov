import logo from './authgovlogo.svg';
import './App.css';
function App() {
  return (
    <div className="AuthGov">
      <header className="AuthGov">
          <div className={'flex-box'}>
              <img src={logo} alt={"App-logo"} className={'App-logo'}/>
              <div className={'title'}>AuthGov</div>
              <div className={'subtitle'}>by Episafe</div>
          </div>
          <div style={{
              textAlign: "center"}}>
        <form>
            <label>Public Key:</label>
            <input type={'text'} id={'publicKey'} name={'publicKey'}/><br/>
            <label>Private Key:</label>
            <input type={'text'} id={'privateKey'} name={'privateKey'}/><br/><br/>
            <input type={'submit'} value={'Submit'}/>
        </form>
          </div>
      </header>
    </div>
  );
}

export default App;
