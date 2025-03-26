import { useState, useEffect } from "react";

function RandomNumber() {
    const [number, setNumber] = useState([]);

    useEffect(() => {
        
        const fetchRandomNumber = async () => {
            try {
                const response = await fetch("/api/RandomNumber"); // ✅ Ensure lowercase if API is case-sensitive
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }

                const data = await response.json();
                console.log("Fetched data:", data); // ✅ Debugging log
                setNumber(data.number);
            } catch (error) {
                console.error("Error fetching random number:", error);
            }
        };

        fetchRandomNumber();
    }, []);

    return (
        <div>
            <h1>Random Number: {number !== null ? number : "Loading..."}</h1>
        </div>
    );
}

export default RandomNumber;
