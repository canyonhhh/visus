import { useEffect, useState } from "react";
import RandomNumber from "./components/RandomNumber"; // Import the RandomNumber component

function App() {
  return (
      <div>
        <h1>Random Number Generator</h1>
        <RandomNumber /> {/* Render the component */}
      </div>
  );
}

export default App;
