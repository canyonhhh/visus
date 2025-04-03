import { useState, useEffect } from "react";

// Define a type for the API response
interface RandomNumberResponse {
    number: number;
}

function RandomNumber() {
    const [number, setNumber] = useState<number | null>(null); // number can be null initially

    useEffect(() => {
        const fetchRandomNumber = async () => {
            try {
                const response = await fetch("/api/RandomNumber"); // Ensure the URL matches your backend
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }

                const data: RandomNumberResponse = await response.json(); // Type the response as RandomNumberResponse
                console.log("Fetched data:", data); // Debugging log
                setNumber(data.number); // Set the fetched number
            } catch (error) {
                console.error("Error fetching random number:", error);
            }
        };

        fetchRandomNumber();
    }, []); // Empty dependency array to run once when component mounts

    return (
        <div>
            <h1 className="text-xl font-bold underline">
                {/* Conditionally render the number or loading message */}
                Random Number: {number !== null ? number : "Loading..."}
            </h1>
        </div>
    );
}

export default RandomNumber;
